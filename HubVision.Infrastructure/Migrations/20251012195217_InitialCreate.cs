using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubVision.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HV");

            migrationBuilder.CreateTable(
                name: "Agency",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    plan = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: true),
                    website = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    trial_ends_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    photo_url = table.Column<string>(type: "text", nullable: false),
                    company = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_agency_agency_id",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrafficManager",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    photo_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    role = table.Column<string>(type: "text", nullable: false, defaultValue: "Junior"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    hired_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    last_login_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_traffic_manager", x => x.id);
                    table.ForeignKey(
                        name: "FK_TrafficManagers_Agencies",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientAssignments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    traffic_manager_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    assigned_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    assigned_by = table.Column<Guid>(type: "uuid", nullable: false),
                    removed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_assignments", x => x.id);
                    table.ForeignKey(
                        name: "FK_ClientAssignments_Agencies",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientAssignments_Clients",
                        column: x => x.client_id,
                        principalSchema: "HV",
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientAssignments_TrafficManagers",
                        column: x => x.traffic_manager_id,
                        principalSchema: "HV",
                        principalTable: "TrafficManager",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformAccount",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    traffic_manager_id = table.Column<Guid>(type: "uuid", nullable: false),
                    platform = table.Column<string>(type: "text", nullable: false),
                    platform_user_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    platform_email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    display_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    access_token = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    refresh_token = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    token_expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    connected_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    last_synced_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    platform_metadata = table.Column<string>(type: "jsonb", nullable: true),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_platform_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlatformAccounts_Agencies",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformAccounts_TrafficManagers",
                        column: x => x.traffic_manager_id,
                        principalSchema: "HV",
                        principalTable: "TrafficManager",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdAccount",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    platform_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ad_account_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "ID da conta de anúncios na plataforma (ex: act_123456)"),
                    account_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Nome da conta de anúncios"),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false, defaultValue: "BRL", comment: "Moeda da conta (ISO 4217)"),
                    time_zone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: "America/Sao_Paulo", comment: "Fuso horário da conta"),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Active", comment: "Status da conta: 1=Active, 2=Disabled, 3=Suspended, 4=Closed"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true, comment: "Se a conta está ativa no sistema"),
                    client_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()", comment: "Data de criação no sistema"),
                    last_synced_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Data da última sincronização com a plataforma"),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ad_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_AdAccounts_Agencies",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdAccounts_Clients",
                        column: x => x.client_id,
                        principalSchema: "HV",
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AdAccounts_PlatformAccounts",
                        column: x => x.platform_account_id,
                        principalSchema: "HV",
                        principalTable: "PlatformAccount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdAccountAccess",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ad_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    traffic_manager_id = table.Column<Guid>(type: "uuid", nullable: false),
                    access_level = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    granted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    granted_by = table.Column<Guid>(type: "uuid", nullable: false),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ad_account_access", x => x.id);
                    table.ForeignKey(
                        name: "FK_AdAccountAccess_TrafficManagers",
                        column: x => x.traffic_manager_id,
                        principalSchema: "HV",
                        principalTable: "TrafficManager",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdAccountAccesses_AdAccounts",
                        column: x => x.ad_account_id,
                        principalSchema: "HV",
                        principalTable: "AdAccount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ad_account_access_agencies_agency_id",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    objective = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    daily_budget = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, comment: "Orçamento diário"),
                    lifetime_budget = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true, comment: "Orçamento vitalício (opcional)"),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ad_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_campaign", x => x.id);
                    table.ForeignKey(
                        name: "FK_Campaigns_AdAccounts",
                        column: x => x.ad_account_id,
                        principalSchema: "HV",
                        principalTable: "AdAccount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_campaign_agency_agency_id",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdSet",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "Nome do conjunto de anúncios"),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Status: ACTIVE, PAUSED, DELETED, etc"),
                    daily_budget = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, comment: "Orçamento diário"),
                    lifetime_budget = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true, comment: "Orçamento vitalício (opcional)"),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data/hora de início"),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Data/hora de término (opcional)"),
                    targeting_json = table.Column<string>(type: "jsonb", nullable: true, comment: "Configuração de segmentação em JSON"),
                    optimization_goal = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Meta de otimização (ex: CONVERSIONS, LINK_CLICKS)"),
                    billing_event = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Evento de cobrança (ex: IMPRESSIONS, LINK_CLICKS)"),
                    campaign_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()", comment: "Data de criação no sistema"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()", comment: "Data da última atualização"),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ad_set", x => x.id);
                    table.CheckConstraint("CK_AdSets_DailyBudget_Positive", "\"daily_budget\" >= 0");
                    table.CheckConstraint("CK_AdSets_EndTime_After_StartTime", "\"end_time\" IS NULL OR \"end_time\" >= \"start_time\"");
                    table.CheckConstraint("CK_AdSets_LifetimeBudget_Positive", "\"lifetime_budget\" IS NULL OR \"lifetime_budget\" >= 0");
                    table.ForeignKey(
                        name: "FK_AdSets_Agencies",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdSets_Campaigns",
                        column: x => x.campaign_id,
                        principalSchema: "HV",
                        principalTable: "Campaign",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ad",
                schema: "HV",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    creative_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "ID do criativo na plataforma"),
                    image_url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    video_url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ad_text = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    headline = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    call_to_action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    link_url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ad_set_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ad", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ad_Agencies",
                        column: x => x.agency_id,
                        principalSchema: "HV",
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ads_AdSets",
                        column: x => x.ad_set_id,
                        principalSchema: "HV",
                        principalTable: "AdSet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_AdSetId_Status",
                schema: "HV",
                table: "Ad",
                columns: new[] { "ad_set_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_AgencyId_AdSetId",
                schema: "HV",
                table: "Ad",
                columns: new[] { "agency_id", "ad_set_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_AgencyId_Status",
                schema: "HV",
                table: "Ad",
                columns: new[] { "agency_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_CreatedAt",
                schema: "HV",
                table: "Ad",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_CreativeId",
                schema: "HV",
                table: "Ad",
                column: "creative_id");

            migrationBuilder.CreateIndex(
                name: "ix_ad_account_client_id",
                schema: "HV",
                table: "AdAccount",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_AdAccounts_AgencyId_AdAccountId",
                schema: "HV",
                table: "AdAccount",
                columns: new[] { "agency_id", "ad_account_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdAccounts_AgencyId_ClientId",
                schema: "HV",
                table: "AdAccount",
                columns: new[] { "agency_id", "client_id" });

            migrationBuilder.CreateIndex(
                name: "IX_AdAccounts_AgencyId_IsActive",
                schema: "HV",
                table: "AdAccount",
                columns: new[] { "agency_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "IX_AdAccounts_PlatformAccountId",
                schema: "HV",
                table: "AdAccount",
                column: "platform_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_ad_account_access_ad_account_id",
                schema: "HV",
                table: "AdAccountAccess",
                column: "ad_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_ad_account_access_traffic_manager_id",
                schema: "HV",
                table: "AdAccountAccess",
                column: "traffic_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_AdAccountAccess_AgencyId_AdAccountId",
                schema: "HV",
                table: "AdAccountAccess",
                columns: new[] { "agency_id", "ad_account_id" });

            migrationBuilder.CreateIndex(
                name: "IX_AdAccountAccess_AgencyId_TrafficManagerId",
                schema: "HV",
                table: "AdAccountAccess",
                columns: new[] { "agency_id", "traffic_manager_id" });

            migrationBuilder.CreateIndex(
                name: "IX_AdAccountAccess_GrantedAt",
                schema: "HV",
                table: "AdAccountAccess",
                column: "granted_at");

            migrationBuilder.CreateIndex(
                name: "IX_AdSets_AgencyId_CampaignId",
                schema: "HV",
                table: "AdSet",
                columns: new[] { "agency_id", "campaign_id" });

            migrationBuilder.CreateIndex(
                name: "IX_AdSets_AgencyId_Status",
                schema: "HV",
                table: "AdSet",
                columns: new[] { "agency_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_AdSets_CampaignId",
                schema: "HV",
                table: "AdSet",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_AdSets_CreatedAt",
                schema: "HV",
                table: "AdSet",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_AdSets_StartTime_EndTime",
                schema: "HV",
                table: "AdSet",
                columns: new[] { "start_time", "end_time" });

            migrationBuilder.CreateIndex(
                name: "IX_AdSets_TargetingJson",
                schema: "HV",
                table: "AdSet",
                column: "targeting_json")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "idx_agency_code",
                schema: "HV",
                table: "Agency",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_agency_name",
                schema: "HV",
                table: "Agency",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_campaign_ad_account_id",
                schema: "HV",
                table: "Campaign",
                column: "ad_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_campaign_agency_id",
                schema: "HV",
                table: "Campaign",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_agency_id",
                schema: "HV",
                table: "Client",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_assignments_traffic_manager_id",
                table: "ClientAssignments",
                column: "traffic_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAssignments_AgencyId_ClientId",
                table: "ClientAssignments",
                columns: new[] { "agency_id", "client_id" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientAssignments_AgencyId_TrafficManagerId",
                table: "ClientAssignments",
                columns: new[] { "agency_id", "traffic_manager_id" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientAssignments_AssignedAt",
                table: "ClientAssignments",
                column: "assigned_at");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAssignments_ClientId_IsPrimary_RemovedAt",
                table: "ClientAssignments",
                columns: new[] { "client_id", "is_primary", "removed_at" },
                filter: "\"removed_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAssignments_ClientId_TrafficManagerId",
                table: "ClientAssignments",
                columns: new[] { "client_id", "traffic_manager_id" });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAccounts_AgencyId_Platform_IsActive",
                schema: "HV",
                table: "PlatformAccount",
                columns: new[] { "agency_id", "platform", "is_active" });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAccounts_AgencyId_TrafficManagerId",
                schema: "HV",
                table: "PlatformAccount",
                columns: new[] { "agency_id", "traffic_manager_id" });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAccounts_Platform_PlatformUserId",
                schema: "HV",
                table: "PlatformAccount",
                columns: new[] { "platform", "platform_user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAccounts_PlatformEmail",
                schema: "HV",
                table: "PlatformAccount",
                column: "platform_email");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAccounts_PlatformMetadata",
                schema: "HV",
                table: "PlatformAccount",
                column: "platform_metadata")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAccounts_TrafficManagerId",
                schema: "HV",
                table: "PlatformAccount",
                column: "traffic_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficManagers_AgencyId_Email",
                schema: "HV",
                table: "TrafficManager",
                columns: new[] { "agency_id", "email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrafficManagers_AgencyId_IsActive",
                schema: "HV",
                table: "TrafficManager",
                columns: new[] { "agency_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "IX_TrafficManagers_AgencyId_Role",
                schema: "HV",
                table: "TrafficManager",
                columns: new[] { "agency_id", "role" });

            migrationBuilder.CreateIndex(
                name: "IX_TrafficManagers_Email",
                schema: "HV",
                table: "TrafficManager",
                column: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ad",
                schema: "HV");

            migrationBuilder.DropTable(
                name: "AdAccountAccess",
                schema: "HV");

            migrationBuilder.DropTable(
                name: "ClientAssignments");

            migrationBuilder.DropTable(
                name: "AdSet",
                schema: "HV");

            migrationBuilder.DropTable(
                name: "Campaign",
                schema: "HV");

            migrationBuilder.DropTable(
                name: "AdAccount",
                schema: "HV");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "HV");

            migrationBuilder.DropTable(
                name: "PlatformAccount",
                schema: "HV");

            migrationBuilder.DropTable(
                name: "TrafficManager",
                schema: "HV");

            migrationBuilder.DropTable(
                name: "Agency",
                schema: "HV");
        }
    }
}
