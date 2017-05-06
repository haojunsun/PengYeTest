namespace PengYe.Project.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CatchLine = c.String(maxLength: 30),
                        TitleImageUrl = c.String(),
                        Body = c.String(),
                        CreatedUtc = c.DateTime(),
                        ChannelTags = c.String(),
                        ColumnTags = c.String(),
                        IsDraft = c.Int(nullable: false),
                        IsRelease = c.Int(nullable: false),
                        OtherImageUrl = c.String(),
                        ImageUrl = c.String(),
                        ManagerName = c.String(),
                        MediaUrl = c.String(),
                    })
                .PrimaryKey(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Articles");
        }
    }
}
