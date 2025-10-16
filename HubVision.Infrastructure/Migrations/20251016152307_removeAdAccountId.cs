using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeAdAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdAccounts_AgencyId_AdAccountId",
                schema: "HV",
                table: "AdAccount");

            migrationBuilder.DropColumn(
                name: "ad_account_id",
                schema: "HV",
                table: "AdAccount");

            migrationBuilder.AlterColumn<string>(
                name: "photo_url",
                schema: "HV",
                table: "TrafficManager",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                schema: "HV",
                table: "TrafficManager",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "refresh_token",
                schema: "HV",
                table: "PlatformAccount",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "display_name",
                schema: "HV",
                table: "PlatformAccount",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "HV",
                table: "Campaign",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "objective",
                schema: "HV",
                table: "Campaign",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "website",
                schema: "HV",
                table: "Agency",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "logo_url",
                schema: "HV",
                table: "Agency",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "optimization_goal",
                schema: "HV",
                table: "AdSet",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Meta de otimização (ex: CONVERSIONS, LINK_CLICKS)",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Meta de otimização (ex: CONVERSIONS, LINK_CLICKS)");

            migrationBuilder.AlterColumn<string>(
                name: "billing_event",
                schema: "HV",
                table: "AdSet",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Evento de cobrança (ex: IMPRESSIONS, LINK_CLICKS)",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Evento de cobrança (ex: IMPRESSIONS, LINK_CLICKS)");

            migrationBuilder.AlterColumn<string>(
                name: "time_zone",
                schema: "HV",
                table: "AdAccount",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "America/Sao_Paulo",
                comment: "Fuso horário da conta",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "America/Sao_Paulo",
                oldComment: "Fuso horário da conta");

            migrationBuilder.CreateIndex(
                name: "IX_AdAccounts_AgencyId_AdAccountId",
                schema: "HV",
                table: "AdAccount",
                columns: new[] { "agency_id", "id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdAccounts_AgencyId_AdAccountId",
                schema: "HV",
                table: "AdAccount");

            migrationBuilder.AlterColumn<string>(
                name: "photo_url",
                schema: "HV",
                table: "TrafficManager",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                schema: "HV",
                table: "TrafficManager",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "refresh_token",
                schema: "HV",
                table: "PlatformAccount",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "display_name",
                schema: "HV",
                table: "PlatformAccount",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "HV",
                table: "Campaign",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "objective",
                schema: "HV",
                table: "Campaign",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "website",
                schema: "HV",
                table: "Agency",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "logo_url",
                schema: "HV",
                table: "Agency",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "optimization_goal",
                schema: "HV",
                table: "AdSet",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                comment: "Meta de otimização (ex: CONVERSIONS, LINK_CLICKS)",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldComment: "Meta de otimização (ex: CONVERSIONS, LINK_CLICKS)");

            migrationBuilder.AlterColumn<string>(
                name: "billing_event",
                schema: "HV",
                table: "AdSet",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                comment: "Evento de cobrança (ex: IMPRESSIONS, LINK_CLICKS)",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldComment: "Evento de cobrança (ex: IMPRESSIONS, LINK_CLICKS)");

            migrationBuilder.AlterColumn<string>(
                name: "time_zone",
                schema: "HV",
                table: "AdAccount",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "America/Sao_Paulo",
                comment: "Fuso horário da conta",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "America/Sao_Paulo",
                oldComment: "Fuso horário da conta");

            migrationBuilder.AddColumn<string>(
                name: "ad_account_id",
                schema: "HV",
                table: "AdAccount",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "ID da conta de anúncios na plataforma (ex: act_123456)");

            migrationBuilder.CreateIndex(
                name: "IX_AdAccounts_AgencyId_AdAccountId",
                schema: "HV",
                table: "AdAccount",
                columns: new[] { "agency_id", "ad_account_id" },
                unique: true);
        }
    }
}
