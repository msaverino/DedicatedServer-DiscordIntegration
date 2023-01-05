using MySqlConnector;
using System.Data;

namespace AdminPanel
{
    public partial class AdminCenter : Form
    {
        public AdminCenter()
        {
            InitializeComponent();
        }


        private void button_connect_Click(object sender, EventArgs e)
        {
            LogLabel("Connecting to Database");
            string connectionString = CS.ReturnConnectionString();
            try
            {
                using var mysqlconnection = new MySqlConnection(connectionString);
                try
                {
                    mysqlconnection.Open();
                    //MessageBox.Show("Connection successful!");
                    // We want to read the table "discord" and get the "token" and "game" columns
                    string query = "SELECT * FROM discord";
                    MySqlCommand command;
                    using (command = new MySqlCommand(query, mysqlconnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // We want to set the textboxes to the values we got from the database
                                textBox_sql_bot_id.Text = reader.GetInt32(0).ToString();
                                textBox_sql_bot_token.Text = reader.GetString(1);
                                textBox_sql_bot_status.Text = reader.GetString(2);
                                textBox_sql_bot_game.Text = reader.GetString(3);
                                textBox_sql_bot_displayName.Text = reader.GetString(4);
                            }
                        }
                    }

                    // We need to update the dataView table with the information from the various SQL databases.
                    query = "SELECT " +
                        "g.game_key, g.game_name, " +
                        "n.server_ip, n.server_port, n.query_port_send, n.query_port_receive, n.query_port_two_way, " +
                        "u.enable_updater, u.updater_path, u.updater_arguments, u.root_path, u.application_path, u.steam_game, u.server_version, u.latest_version, u.update_lock, u.force_update, u.dedicated_acknowledgement, dedicated_acknowledgement_date, dedicated_completed_update, " +
                        "dr.role, " +
                        "dn.admin_channel, dn.notify_admins, dn.user_notification_channel, dn.notify_users, dn.player_count_channel, dn.update_player_count, " +
                        "pc.online_players, pc.last_updated, pc.most_online, pc.most_online_date " +
                        "FROM games g " +
                        "JOIN updater u ON g.game_key = u.game_key " +
                        "JOIN discord_notification dn ON g.game_key = dn.game_key " +
                        "JOIN discord_roles dr ON g.game_key = dr.game_key " +
                        "JOIN networking n ON g.game_key = n.game_key " +
                        "JOIN player_count pc ON g.game_key = pc.game_key ";

                    command = new MySqlCommand(query, mysqlconnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGrid_sql_output.DataSource = table;
                    //dataGrid_sql_output.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);

                    mysqlconnection.Close();
                    LogLabel("Connection successful!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_sql_bot_update_Click(object sender, EventArgs e)
        {
            LogLabel("Updating Bot Information");
            string connectionString = CS.ConnectionString;
            try
            {

                // Using the same connection string we used to connect to the database
                using var mysqlconnection = new MySqlConnection(connectionString);
                try
                {
                    mysqlconnection.Open();

                    // We need to update the Discord Bot Inofmration
                    string id = textBox_sql_bot_id.Text;
                    string token = textBox_sql_bot_token.Text;
                    string status = textBox_sql_bot_status.Text;
                    string game = textBox_sql_bot_game.Text;
                    string displayName = textBox_sql_bot_displayName.Text;

                    string query = $"UPDATE `development`.`discord` SET `token`='{token}', `status`='{status}', `game`='{game}', `display_name`='{displayName}' WHERE  `discord_key`=1";

                    //string query = $"DELETE FROM games WHERE game_key = {dataGrid_sql_games.SelectedRows[0].Cells[0].Value}";
                    // Give the user a confirmation message
                    using (var command = new MySqlCommand(query, mysqlconnection))
                    {
                        command.ExecuteNonQuery();
                    }
                    mysqlconnection.Close();
                    LogLabel("Bot Information Updated");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button_sql_game_delete_Click(object sender, EventArgs e)
        {
            LogLabel("Deleting Game from Database");
            string connectionString = CS.ConnectionString;
            // Using the same connection string we used to connect to the database
            using var mysqlconnection = new MySqlConnection(connectionString);
            try
            {
                mysqlconnection.Open();

                if (dataGrid_sql_output.SelectedRows.Count == 0)
                {
                    dataGrid_sql_output.Rows[0].Selected = true;
                }
                string query = $"DELETE FROM games WHERE game_key = {dataGrid_sql_output.SelectedRows[0].Cells[0].Value}";
                string gameName = dataGrid_sql_output.SelectedRows[0].Cells[1].Value.ToString();
                // Give the user a confirmation message
                DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete {gameName}?", "Delete Game", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (var command = new MySqlCommand(query, mysqlconnection))
                    {
                        command.ExecuteNonQuery();
                        LogLabel($"deleted {gameName}");
                        MessageBox.Show("Delete successful!");
                    }
                }
                else
                {
                    LogLabel("User aborted deleting " + gameName);
                }

                mysqlconnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdminCenter_Load(object sender, EventArgs e)
        {

        }

        private void LogLabel(string message)
        {
            label_logger.Text = message;
        }

        private void dataGrid_sql_output_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}