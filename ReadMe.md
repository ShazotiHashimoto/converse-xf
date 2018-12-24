# Converse – Decentralized Chat

## About
Converse is the first decentralized chat running on the TRON blockchain. Due to the speed of the TRON network, it is possible to run any transaction a user makes through the blockchain and encrypt it with the technology that the blockchain provides, instead of using a central intermediary to transport messages.

## Screenshots
![ScreenShot](https://raw.githubusercontent.com/Dryec/converse-xf/master/screenshots/welcome_page.jpg)
![ScreenShot](https://raw.githubusercontent.com/Dryec/converse-xf/master/screenshots/register_page.jpg)
![ScreenShot](https://raw.githubusercontent.com/Dryec/converse-xf/master/screenshots/chat_overview_page.jpg)
![ScreenShot](https://raw.githubusercontent.com/Dryec/converse-xf/master/screenshots/chat_page.jpg)
![ScreenShot](https://raw.githubusercontent.com/Dryec/converse-xf/master/screenshots/user_info_popup.jpg)

## How it works
We use a direct connection to the node via GRPC for message transmission. It is transmitted through a token transaction where the app fills the data field with message information in JSON format and encrypts private information.
These token transactions are then processed by the TRON network and once a block has been created for them, our server scans each block for transactions with our token and processes the data and notifies the app to receive all messages directly.

## What are the advantages
By using the blockchain, messages are kept decentralized and through encryption, only the owners of the private keys of the sender and recipient will be able to read the messages.
An example would be chats where messages are sensitive and important, in such a case none of the chat partners can pretend fraud by deleting chat messages or manipulating them through a centralized system.
Even if our server is shut down, messages can still be retrieved through the blockchain.
Our server acts exclusively as an interface for faster message retrieval as it synchronizes the blockchain and notifies users.

## The added value for TRON
Since every message, be it a username set, profile picture set or a chat message, is a transaction on the TRON network and these transactions are not only worth air but contain a real value on the basis of private messages, which users can still access years later and therefore the TRON blockchain receives another real benefit.
An example for the quantity of transactions would be with estimated 5000 users with an average number of 100 messages per user per day and active groups, estimated 500 000 transactions per day.
