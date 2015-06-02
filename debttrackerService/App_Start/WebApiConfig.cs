using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using debttrackerService.DataObjects;
using debttrackerService.Models;
using debttrackerService.Authentication;

namespace debttrackerService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            Database.SetInitializer(new debttrackerInitializer());

            // Allow authentication to happen locally for debug purposes
            config.SetIsHosted(true);
        }
    }

    public class debttrackerInitializer : ClearDatabaseSchemaIfModelChanges<debttrackerContext>
    {
        protected override void Seed(debttrackerContext context)
        {
            // seed todoItems
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            // seed Accounts
            var salt = CustomLoginProviderUtils.generateSalt();
            Account accountA = new Account()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "seedaccount",
                Salt = salt,
                SaltedAndHashedPassword = CustomLoginProviderUtils.hash("seedaccount", salt)
            };
            Account accountB = new Account()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "seedaccount",
                Salt = salt,
                SaltedAndHashedPassword = CustomLoginProviderUtils.hash("seedaccount", salt)
            };            

            // seed Users
            var userA = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Account = accountA,
                Groups = new List<Group>()
            };
            var userB = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Account = accountB,
                Groups = new List<Group>()
            };

            // seed Friends
            var friend = new Friend()
            {
                Id = Guid.NewGuid().ToString(),
                UserA = userA,
                UserB = userB,
                Status = FriendStatus.Accepted
            };            

            // seed Groups
            var group = new Group()
            {
                Id = Guid.NewGuid().ToString(),
                Users = new List<User>() { userA, userB },
                Debts = new List<Debt>()
            }; 

            // seed Debts
            var debt = new Debt()
            {
                Id = Guid.NewGuid().ToString(),
                SourceUser = userA,
                TargetUser = userB,
                Amount = 10.00,
                Description = "seed debt",
                Group = group
            };

            userA.Groups.Add(group);
            userB.Groups.Add(group);
            group.Debts.Add(debt);

            context.Set<Account>().Add(accountA);
            context.Set<Account>().Add(accountB);

            context.Set<User>().Add(userA);
            context.Set<User>().Add(userB);

            context.Set<Friend>().Add(friend);

            context.Set<Debt>().Add(debt);

            context.Set<Group>().Add(group);

            base.Seed(context);
        }
    }
}

