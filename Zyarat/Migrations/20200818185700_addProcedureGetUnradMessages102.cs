using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class addProcedureGetUnradMessages102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create proc GetUnReadMessages(@repId as int)
AS 
BEGIN
DECLARE @globalMessage int ;
DECLARE @messages int;
 
select @globalMessage= COUNT (*)
FROM GlobalMessages g LEFT JOIN (select * from GlobalMessageReading where ReaderId=@repId)
as r ON r.GlobalMessageId=g.Id
WHERE ReaderId is null

select @messages=COUNT(*)
FROM Messages WHERE [Read]=0 and ReceiverId=@repId

select @messages+@globalMessage;
END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
