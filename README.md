# Kurkku

A private Habbo Hotel server designed for emulating the 2013 version of Habbo written in C# using .NET Core. The SWF release it currently uses is *RELEASE63-201302211227-193109692* but this may be subject to change in the future.
 
Kurkku uses DotNetty for asynchronous TCP sockets and networking. Fluid NHibernate for quick and easy database access and SQL queries on the fly without having to manually write queries and Newtonsoft.Json for JSON serializing and deserializing for various custom item attributes. 

**This project is in active development and not looking for any pull requests or contributions right now**
 
# Features

**User**

- User login with SSO including SSO expiry
- User logout
- User information retrieval
- Seasonal currency support

**Messenger**

- Add friend
- Remove friend
- Send friend request
- Remove friend request
- Accept friend request
- Instant message friend
- Update friend status when logging in/out/entering rooms
- Follow friend

**Rooms**

- Promotable room display supported
- Random promotion to display on other navigator tabs
- Enter room
- Walk in room
- Leave room
- Multiple user support in room
- Chatting in room
- Item walking collision
- Allow walkthrough user setting (can't save settings yet)

**Catalogue**

- Catalogue discounts
- Purchase items (including bulk purchase and discount calculation)
- Trophy purchase supported
- Post-its supported
- Seasonal currency supported

**Items**

- Place floor items
- Place wall items
- Move floor items
- Move wall items
- Pick up floor items
- Pickup wall items

**Special Item Interactions**

- Place stickies/post-its
- Modify post-it
- View trophy message in room

# Important Information

The MySQL/MariaDB event scheduler must be enabled in order for values to be refreshed in the database.

You can enable the event scheduler by running the code below and then restarting the database server.

```SET GLOBAL event_scheduler = ON;```
 
# Screenshot

![](https://cdn.discordapp.com/attachments/531015659027562505/703500593376788551/Dw8dOQC8RU.gif)

![](https://cdn.discordapp.com/attachments/531015659027562505/703841326373797888/unknown.png)
