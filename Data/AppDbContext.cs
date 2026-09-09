//import statements
using Microsoft.EntityFrameworkCore;
using backEnd.Models;


namespace backEnd.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //declare/register all models
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatMembers> ChatMembers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<KeywordCategory> KeywordCategories { get; set; }
    public DbSet<MessageFlag> MessageFlags { get; set; }
    public DbSet<MessageReaction> MessageReactions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    //make the guid fields automatically created via the db

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Attachment>(entity =>
        {
            //primary key
            entity.HasKey(a=> a.Id);

            //auto generate primary key
            entity.Property(a => a.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            //index message ids which the attachment is linked to
            entity.HasIndex(a => a.MessageId);

        });


        modelBuilder.Entity<Chat>(entity =>{

            //primary key, automatically indexes it
            entity.HasKey(c=>c.Id);
            //auto generate id
            entity.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            
        });


        modelBuilder.Entity<ChatMembers>(entity =>
        {
            //has primary key
            entity.HasKey(cm => cm.Id);

            //auto generate primary key
            entity.Property(cm => cm.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            //index chat foreign id 
            entity.HasIndex(cm=>cm.ChatId);

            //index user foreign id
            entity.HasIndex(cm=>cm.UserId);

        });


        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.HasKey(k=>k.Id);

            entity.Property(k=>k.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            entity.HasIndex(k=>k.CategoryId);
        });

        
        modelBuilder.Entity<KeywordCategory>(entity =>
        {
            //auto generate primary key
            entity.HasKey(kc=>kc.Id);
            entity.Property(kc=>kc.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        });

        modelBuilder.Entity<Message>(entity =>
        {
            //auto generate primary key
            entity.HasKey(m=>m.Id);
            entity.Property(m=>m.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            //indexed sender ID
            entity.HasIndex(m=>m.SenderId);
            entity.HasIndex(m=>m.ChatId);

            
        });


        modelBuilder.Entity<MessageFlag>(entity =>
        {
            //auto generate primary key
            entity.HasKey(mf=>mf.Id);
            entity.Property(mf=>mf.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            //index category it belongs to
            entity.HasIndex(mf=>mf.CategoryId);
        });

        modelBuilder.Entity<MessageReaction>(entity =>
        {
            //auto generate primary key
            entity.HasKey(mr=>mr.Id);
            entity.Property(mr=>mr.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            entity.HasIndex(mr=>mr.MessageId);
            entity.HasIndex(mr=>mr.SenderId);

            
        });


        modelBuilder.Entity<Notification>(entity =>
        {
            //auto generate primary key
            entity.HasKey(n=>n.Id);
            entity.Property(n=>n.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            entity.HasIndex(n=>n.ReceiverId);
            entity.HasIndex(n=>n.ChatId);
            entity.HasIndex(n=>n.MessageId);

            
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(rt=>rt.Id);
            entity.Property(rt =>rt.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            entity.HasIndex(rt=>rt.UserId);
        });

        //user
        modelBuilder.Entity<User>(entity =>
        {
           //primary key
           entity.HasKey(u=> u.Id);

           //generate Id automatically on create
           entity.Property(u=>u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

           //unique and indexed username
           entity.HasIndex(u => u.UserName).IsUnique();

           //unique and indexed email
           entity.HasIndex(u => u.Email).IsUnique();
        });

    }



}