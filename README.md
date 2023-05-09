# chatbot
This application allows several users to talk in a chatroom and also to get stock quotes
from an API using a specific command.

<h3> Endpoints </h3>

- `GET /getbystock` <br>
  Returns the specific info related to the stock provided.
- `POST /addmessage` <br>
  Add a message to the chat.
- `GET /getmessages{chatRoomId}` <br>
  Returns all messages from the respective chatroom (chatRoomId).
- `POST /createuser` <br>
  Create a new user.
- `POST /checkuser` <br>
  Check if the provided user credentials are valid.

<h1> 🚀 Starting </h1>
These instructions will allow you to get a copy of the project in your local for development and testing. </br>

</br>
<pre> <code> 
1. Add a Cosmos DB string within <i>CosmosDB</i> variable in appsettings.json file.
2. Add a Azure Service Bus string within <i>QueueEndpoint</i> variable in appsettings.json file.
</code> </pre>

<h1> 🛠️ Developed with </h1>
.NET 6 </br>
Mongo DB </br>
Message Broker </br>
