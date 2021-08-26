# Review

Since there is no pull request, all code review is done in code, using comments. Some points that go beyond that code I explained here.

## The Some Points

* Used stored procedures, even transactions
* Didn't use entities - used parameters for primitives instead. There **are** entities, but they are used only on DAL level only for them to be transformed into a string
* Website is StYlEd (poorly, there's black text on dark background in Firefox, but login page looks good)
* No role management, it was in the task
* logs are only on DAL level (they log user actions, that's good (though you don't know which user did what), but if something fails in BLL and PL, you'll never know about that from logs)
* The App is not stable. Implemented architecture doesn't use benefits of types and OOP, and therefore has a lot of room for bugs. Plus web pages don't use parameters (query part of url), so a simple page reload crashes it.

### Good commit messages (mostly)

They do a good job explaining what commits change, but sometimes they have way too many info.

Like here:

> **Added dal actions logger(I think such logger is enough, my application is a simple web-site, that's why I don't think it's necessary to log all actions on levels, that are higher than DAL).**
>
> Also fixed bug, when user saw it's mark for a nonrated recipe(problem consisted in the wrong declaration of stored proc "GetUserAward". This proc didn't appeal to a recipeId, but it what was the source of problem.)

This could be reduced to just 

> **Added logger for DAL and fixed fake rating bug**
>
> seems like it was GetUserAward proc causing this bug

There are 2 things:
* commit message title is short enough (<50-70 characters recommended, Fork shows if you exceed)
* commit message description (everything after first blank line) is straight to the point, explains some nuances, but doesn't go too deep - no one is going to read that if it's too long. Other thing we can see from actual code diff


### No contract in project names / no structure

When I opened the repository I saw folders and didn't know wher to start, because some of them were C# projects, some were just folders (with sql files), and the names of folders didn't always tell what they are.
I found out that projects also didn't use namespace naming conventions (like Project.Feature/Layer.Clarification, from common to difference)

| Original                          | What usually is used in repositories   | Comments
|-----------------------------------|----------------------------------------|---------
| OnlineRecipeBook\BL               | src\OnlineRecipeBook.Logic             | (or we use name "Core" in our project)
| OnlineRecipeBook\Common           | src\OnlineRecipeBook.Common
| OnlineRecipeBook\CommonInterfaces | src\OnlineRecipeBook.Common.Interfaces
| OnlineRecipeBook\CommonLogic      | src\OnlineRecipeBook.Common.Logic
| OnlineRecipeBook\DALInterfaces    | src\OnlineRecipeBook.DAL.Interfaces
| OnlineRecipeBook\DataValidator    | src\OnlineRecipeBook.Validation
| OnlineRecipeBook\DB_script        | setup\Sql Database                     | (these sql scripts aren't part of solution - they are needed only once, so we can make a new root folder for that)
| OnlineRecipeBook\Dependencies     | src\OnlineRecipeBook.Dependencies
| OnlineRecipeBook\Entities         | src\OnlineRecipeBook.Entities
| OnlineRecipeBook\IdentityChecker  | src\OnlineRecipeBook.Authentication
| OnlineRecipeBook\OnlineRecipeBook | src\OnlineRecipeBook.Logic.Interfaces
| OnlineRecipeBook\SqlDAL           | src\OnlineRecipeBook.DAL.Sql           | (we can also have OnlineRecipeBook.DAL.MongoDB for example, and namespace would only differ by the last part)
| OnlineRecipeBook\WebPL            | src\OnlineRecipeBook.PL.Web

Also, seems like there's too many projects. I think `Common`, `CommonInterfaces` and `CommonInterfaces` could be in one project (if it grows, you can create new projects and refactor them), `IdentityChecker` could also be integrated somewhere, it's not like you'll have an identity server or some very complex authenticationidentity logic with multiple auth methods, providers, roles, scopes, permissions, tokens, regional settings to comply with laws and other terms, related to identity management --at this point. 

KISS (Keep it stupid simple) and YAGNI (You ain't gonna need it) principles: the simpler - the better (easier to understand and maintain, harder to make a mistake, basically).


### DB scripts

I appreciate the effort of creating these scripts manually, but perhaps you'd find it easier to create DB creatins script by right-clicking on the DB -> Tasks -> Generate scripts. It's also failproof (I couldn't setup my DB just by running your scripts, there were no GO statements). For comments about DB and SQL queries, see sql files


### Pages

1. Pages should be grouped by feature, not by function (not fun searching what you can do with certain entity, and what not). + some pages have a unique function (like reaction) and then they either end up in a folder alone or get in a folder where all pages are different. Grouping by feature (by entity in your case) is easier.
2. You use redirections where you could use IsPost check to actually do a thing without redirections to other page.
3. To save state between redirections you use statics, which is actually a story of bad security:

#### A tale of an authentication interception vulnerability

I'm sure you've heard that statics are evil. Here's one reason why: static members are like the one and only instance of class that can be accessed by anyone from everywhere in code.
In app you use it save data between requests, and particularly to authenticate user. The thing that misses your expectations is that ASP.NET is made to be able to work with multiple users at the same time (using threads).
Since you use statics, these values are accessible in every user thread.

So, me, obviously being a bad hacker-guy was able to use that to my advantage and login as admin without entering credentials!

All I needed to do to reproduce it in controlled environment is: 
* block requests to AuthentificationPage.cshtml in DevTools, 
* press Login button, 
* browser sends credentials to server and it returns 302 response, telling browser to open page AuthentificationPage.cshtml to continue. But I blocked it, so it didn't actually go to that page.
* open another browser and go to AuthentificationPage.cshtml there
* Voila, server finishes authentication using data from static members.

In reality it could be just a bad internet on Bob's side or a situation when Bob and Alice are trying to log in at the same time. If that happened, Alice could log in as Bob, without knowing his passport. That's an actual vulnerability.
This looks similar to a GitHub vulnerability where they had to log everyone out, but their vulnerability wasn't so easy to exploit.

---


Overall, this project implementation is weak. I see that you are confused about programming concepts, but I also see that you've got the spirit for programming. It's almost like you rush to make _something_ to work, but then you stop improving/refactoring it because it works and you have other things to implement. And the same thing with learning - you play with things, but don't apply principles and "best practices".

For now this project is like a stick house like in the Story With Three Little Pigs, with parts made out of straw. Principles, examples and "best practices" help you to make house that is less unique, but made of bricks. After you know how to made these, you can start experimenting and make them out of concrete, and even use beautiful origami in some places, knowing, that it'll not harm.

**I'd suggest you to re-read about OOP and SOLID principles, try to understand them, rewatch latest streams and do this experiment:**

    Look at someone else's repository, see how things are connected there, how is it different from yours and what it takes to understand it. 

    After think of a new feature in your app and try to implement it. This feature should:
    * bring something new
    * and in some way change something you already have
        
    After that, think, what would it take to implement a similar feature in that other repository. Is it more or less confusing? How many changes would it take compared to your project? Does it look better/cleaner?

Damn, that was a lot of text