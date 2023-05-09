using StockBot.MessageBroker;
using StockBot.Model;

namespace StockBot.Service
{
    public class StockBotService
    {
        private HttpClient _client;
        private readonly ISendStockQueue _sendStockQueue;
        private const string stooq_url = "https://stooq.com/q/l/?s=";
        private const string stooq_csv_stub = "&f=sd2t2ohlcv&h&e=csv";

        public StockBotService(HttpClient client, ISendStockQueue sendStockQueue)
        {
            _client = client;
            _sendStockQueue = sendStockQueue;
        }

        public async void GetStock(string stock, string chatRoomId)
        {
            var stockResult = await InvokeStockService(stock, chatRoomId);
            await Task.FromResult(_sendStockQueue.SendMessage(stockResult));
        }

        private async Task<Stock> InvokeStockService(string stock, string chatRoomId)
        {
            using (var httpClient = _client)
            {
                var response = await httpClient.GetAsync($"{stooq_url}{stock}{stooq_csv_stub}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new ArgumentException(errorResponse);
                }
                var content = await response.Content.ReadAsStringAsync();
                var lines = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length <= 1) //If the response contains only headers
                    return new Stock();

                var data = lines[1].Split(',');
                return new Stock()
                {
                    Symbol = data[0],
                    Date = DateTime.TryParse(data[1], out var date) ? date : default,
                    Time = DateTime.TryParse(data[2], out var time) ? time : default,
                    Open = double.TryParse(data[3], out var open) ? open : default,
                    High = double.TryParse(data[4], out var high) ? high : default,
                    Low = double.TryParse(data[5], out var low) ? low : default,
                    Close = double.TryParse(data[6], out var close) ? close : default,
                    Volume = double.TryParse(data[7], out var volume) ? volume : default,
                    ChatRoomId = chatRoomId
                };
            }
        }
    }
}
