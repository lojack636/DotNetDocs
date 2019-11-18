namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(),
                        Phone = c.String(),
                        Maile = c.String(),
                        Birthday = c.DateTime(),
                        Address = c.String(),
                        Sort = c.Int(),
                        Enabled = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(),
                        CreateOperator = c.String(),
                        ModifyTime = c.DateTime(),
                        ModifyOperator = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserEntities");
        }
    }
}
