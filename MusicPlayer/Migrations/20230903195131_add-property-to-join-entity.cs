using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicPlayer.Migrations
{
    /// <inheritdoc />
    public partial class addpropertytojoinentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Playlists_PlaylistId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Songs_SongsSourceId_SongsUserId",
                table: "PlaylistSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong");

            migrationBuilder.RenameTable(
                name: "PlaylistSong",
                newName: "PlaylistsSong");

            migrationBuilder.RenameColumn(
                name: "SongsUserId",
                table: "PlaylistsSong",
                newName: "SongUserId");

            migrationBuilder.RenameColumn(
                name: "SongsSourceId",
                table: "PlaylistsSong",
                newName: "SongSourceId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistSong_SongsSourceId_SongsUserId",
                table: "PlaylistsSong",
                newName: "IX_PlaylistsSong_SongSourceId_SongUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistsSong",
                table: "PlaylistsSong",
                columns: new[] { "PlaylistId", "SongSourceId", "SongUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistsSong_Playlists_PlaylistId",
                table: "PlaylistsSong",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "PlaylistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistsSong_Songs_SongSourceId_SongUserId",
                table: "PlaylistsSong",
                columns: new[] { "SongSourceId", "SongUserId" },
                principalTable: "Songs",
                principalColumns: new[] { "SourceId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistsSong_Playlists_PlaylistId",
                table: "PlaylistsSong");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistsSong_Songs_SongSourceId_SongUserId",
                table: "PlaylistsSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistsSong",
                table: "PlaylistsSong");

            migrationBuilder.RenameTable(
                name: "PlaylistsSong",
                newName: "PlaylistSong");

            migrationBuilder.RenameColumn(
                name: "SongUserId",
                table: "PlaylistSong",
                newName: "SongsUserId");

            migrationBuilder.RenameColumn(
                name: "SongSourceId",
                table: "PlaylistSong",
                newName: "SongsSourceId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistsSong_SongSourceId_SongUserId",
                table: "PlaylistSong",
                newName: "IX_PlaylistSong_SongsSourceId_SongsUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong",
                columns: new[] { "PlaylistId", "SongsSourceId", "SongsUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Playlists_PlaylistId",
                table: "PlaylistSong",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "PlaylistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Songs_SongsSourceId_SongsUserId",
                table: "PlaylistSong",
                columns: new[] { "SongsSourceId", "SongsUserId" },
                principalTable: "Songs",
                principalColumns: new[] { "SourceId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
