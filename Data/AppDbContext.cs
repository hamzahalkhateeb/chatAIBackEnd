//import statements
using Microsoft.EntityFrameworkCore;
using backEnd.Models;
using Microsoft.AspNetCore.Http.Features;
using System.Net.Mail;

namespace backEnd.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users {get; set;}
    public DbSet<Chat> Chats {get; set;}
    public DbSet<ChatMembers> ChatMembers {get; set;}
    public DbSet<Message> Messages {get; set;}
    public DbSet<Notification> Notifications {get; set;}
    public DbSet<Attachment> Attachments {get; set;}
    public DbSet<Keyword> Keywords {get; set;}
    public DbSet<KeywordCategory> KeywordCategories {get; set;}
    public DbSet<MessageFlag> MessageFlags {get; set;}
    public DbSet<MessageReaction> MessageReactions {get; set;}
    public DbSet<RefreshToken> RefreshTokens {get; set;}
    
}