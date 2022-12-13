using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Windows.Forms;
using System.Net.Http;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.IO;
using Newtonsoft.Json;
using System.Data.OleDb;
using System.Threading;
using yt_DesignUI.Components;
using yt_DesignUI.Controls;
using System.Drawing.Drawing2D;
using yt_DesignUI;


//using HtmlDocument = System.Windows.Forms.HtmlDocument;

namespace VK_Parser
{
    public partial class main_form : Form
    {
        //  https://1drv.ms/u/s!Av7Q6DLV9mxygexdG0Iy9vjYWXsYog?e=gofrwr
        public static string way_DB = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = Data_VK_parser.accdb";
        private OleDbConnection connection_DB;

        settings settings = new settings();
        //Animation buttonAnim = new Animation();

        bool global_save_flag = false;
        string global_id_Data = string.Empty;
        int global_rowindex_Data = -1;
        bool global_found_spisok = false;
        bool global_found_profil = false;

        public main_form()
        {
            InitializeComponent();
            Animator.Start();
            connection_DB = new OleDbConnection(way_DB);
            connection_DB.Open();
        }

        private void found_profil_Click_1(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            global_rowindex_Data = -1;
            global_found_profil = true;
            found_or_save_profil(false);
        }

        private void save_profil_Click_1(object sender, EventArgs e)
        {
            found_or_save_profil(true);
        }
        public void found_or_save_profil(bool flag)
        {
            if (find_id.Text != "")
            {
                delete_one_profile.Enabled = true;
                clear_method();
                global_found_spisok = true;
                global_save_flag = flag;
                method_NewtonsoftJson(false);
            }
            else { MessageBox.Show("не введен id", "уведомление"); }
        }
        private async void save_DB(int id, string first_name, string last_name, string sex, string domain)
        {
            string query = "INSERT INTO профиль (id_user, id_авторизация, фамилия, имя, пол, nickname) VALUES ('" + id + "','" + settings.id_authorization + "','" + last_name + "','" + first_name + "','" + sex + "','" + domain + "')";
            OleDbCommand command = new OleDbCommand(query, connection_DB);
            command.ExecuteNonQuery();

            adapterReader("SELECT id_user, id_авторизация, фамилия, имя, пол, nickname, id_профиль FROM профиль WHERE id_авторизация=" + settings.id_authorization, "main_dgv");

            MessageBox.Show("профиль успешно сохранён в базу данных", "успех");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_settings();
            authorization();
            l_reg.Text = "ид авторизации:" + settings.id_authorization;
            adapterReader("SELECT id_user, id_авторизация, фамилия, имя, пол, nickname, id_профиль FROM профиль WHERE id_авторизация=" + settings.id_authorization, "main_dgv");
            dataGridView1.ClearSelection();
        }


        public void adapterReader(string query, string dgv)
        {
            //adapterReader("SELECT id_user, id_авторизация, фамилия, имя, пол, nickname, id_профиль FROM профиль WHERE id_авторизация=" + settings.id_authorization);

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection_DB);
                DataTable table = new DataTable();
                adapter.Fill(table);
                string[] array = { "фамилия", "имя", "пол", "nickname", "id_user", "id_профиль" };
                string[] array1 = { "друзья", "подписчики", "подписчики", "фотографий", "видеозаписей", "аудиозаписей", "сообщетсв", "подарки" };


                dataGridView1.RowCount = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataGridView1.RowCount++;
                    for (int j = 0; j < table.Columns.Count - 1; j++)
                    {
                        dataGridView1[j, i].Value = Convert.ToString(table.Rows[i][array[j]]);
                    }
                }

                dataGridView1.Refresh();
            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
        }
        public void authorization()
        {
            try
            {
                authorization_form authorization_form = new authorization_form();

                if (!settings.authorization || settings.id_authorization == 0)
                {
                    if (authorization_form.ShowDialog() == DialogResult.OK)
                    {
                        if (authorization_form.TB_pass.Text != "" && authorization_form.TB_log.Text != "")
                        {
                            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT id_авторизация, логин, пароль FROM авторизация", connection_DB);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            bool flag = false;
                            int temp_i = 0;

                            for (int i = 0; i < table.Rows.Count; i++)
                                if (authorization_form.TB_log.Text == table.Rows[i]["логин"].ToString() && authorization_form.TB_pass.Text == table.Rows[i]["пароль"].ToString())
                                {
                                    settings.id_authorization = Convert.ToInt32(table.Rows[i]["id_авторизация"]);
                                    flag = true; temp_i = i;
                                    break;
                                }
                            if (!flag)

                                if (MessageBox.Show("профиль " + authorization_form.TB_log.Text + " не существует\n" +
                                    "\n" +
                                    "создать новый?", "уведомление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    string query = "INSERT INTO авторизация (логин, пароль) VALUES ('" + authorization_form.TB_pass.Text + "','" + authorization_form.TB_log.Text + "')";
                                    OleDbCommand command = new OleDbCommand(query, connection_DB);
                                    command = new OleDbCommand(query, connection_DB);
                                    command.ExecuteNonQuery();

                                    MessageBox.Show("авторизация локального пользователя прошла успешно", "успех");
                                }
                                else
                                    authorization();

                            settings.authorization = authorization_form.checkBox1.Checked;
                            save_setting();
                        }
                        else
                        {
                            MessageBox.Show("поля \"логин\" и \"пароль\" обязательны для ввода", "ошибка");
                            authorization();
                        }
                    }
                    else this.Close();
                }
            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }

        }
        public void clear_method()
        {
            pictureBox1.Image = null;
            online.Text = "";
            name.Text = "";
            bdate.Text = "";
            id.Text = "";
            last_update.Clear();
            city.Text = "";
            education.Text = "";
            site.Text = "";
            relation.Text = "";
            TBfriends.Text = "";
            TBfollowers.Text = "";
            contacts.Text = "";
            audios.Text = "";
            pages.Text = "";
            gifts.Text = "";
            photos.Text = "";
            textBox10.Text = "";
            textBox1.Text = "";
            videos.Text = "";
            posts.Text = "";
            domain.Text = "";
            status.Text = "";
            sex.Text = "";
            type_profil.Text = "профиль:";
            type_profil.BackColor = SystemColors.Control;
        }
        public void work_button(bool flag, bool close_profil)
        {
            snimok_profil.Enabled = flag; // сделать снимок
            tendencii.Enabled = flag; // графики

            if (!flag)
            {
                snimok_profil.Enabled = false; // сделать снимок
                tendencii.Enabled = false; // графики

                snimok_profil.BackColor = Color.RosyBrown; // сделать снимок
                tendencii.BackColor = Color.RosyBrown; // графики
            }
            else
            {
                if (!close_profil)
                {
                    snimok_profil.BackColor = Color.Tomato; // сделать снимок
                    tendencii.BackColor = Color.Tomato; // графики
                }
                else
                {
                    snimok_profil.BackColor = Color.RosyBrown; // сделать снимок
                    tendencii.BackColor = Color.RosyBrown; // графики
                }
            }

            if (close_profil)
            {
                snimok_profil.Enabled = false; // сделать снимок
                tendencii.Enabled = false; // графики


                open_spisok_friends.Enabled = false;// список друзей
                open_spisok_followers.Enabled = false; // список подписчиков
                open_view_photo.Enabled = false; // список фотографий


                open_spisok_friends.BackColor = Color.RosyBrown;// список друзей
                open_spisok_followers.BackColor = Color.RosyBrown; // список подписчиков
                open_view_photo.BackColor = Color.RosyBrown; // список фотографий
            }
            else
            {
                open_spisok_friends.BackColor = Color.Tomato;// список друзей
                open_spisok_followers.BackColor = Color.Tomato; // список подписчиков
                open_view_photo.BackColor = Color.Tomato; // список

                open_spisok_friends.Enabled = true;// список друзей
                open_spisok_followers.Enabled = true; // список подписчиков
                open_view_photo.Enabled = true; // список фотографий
            }
        }

        public void TB_focus()
        {
            type_profil.Focus();
            online.Focus();
            domain.Focus();
            id.Focus();
            textBox1.Focus();
            name.Focus();
            status.Focus();
            bdate.Focus();
            city.Focus();
            education.Focus();
            site.Focus();
            relation.Focus();
            sex.Focus();
            contacts.Focus();
            TBfriends.Focus();
            TBfollowers.Focus();
            photos.Focus();
            videos.Focus();
            audios.Focus();
            gifts.Focus();
            pages.Focus();
            posts.Focus();
            textBox10.Focus();
            find_id.Focus();
        }
        public void work_all_button(bool flag)
        {
            dataGridView1.Enabled = flag;
            open_spisok_friends.Enabled = flag;
            open_spisok_followers.Enabled = flag;
            open_view_photo.Enabled = flag;
            paste.Enabled = flag;
            tendencii.Enabled = flag;
            snimok_profil.Enabled = flag;
            delete_one_profile.Enabled = flag;
            found_profil.Enabled = flag;
            save_profil.Enabled = flag;

            if (flag)
            {
                open_spisok_friends.BackColor = Color.Tomato;
                open_spisok_followers.BackColor = Color.Tomato;
                open_view_photo.BackColor = Color.Tomato;
                paste.BackColor = Color.Tomato;
                tendencii.BackColor = Color.Tomato;
                snimok_profil.BackColor = Color.Tomato;
                delete_one_profile.BackColor = Color.Tomato;
                found_profil.BackColor = Color.Tomato;
                save_profil.BackColor = Color.Tomato;
            }
            else
            {
                open_spisok_friends.BackColor = Color.RosyBrown;
                open_spisok_followers.BackColor = Color.RosyBrown;
                open_view_photo.BackColor = Color.RosyBrown;
                paste.BackColor = Color.RosyBrown;
                tendencii.BackColor = Color.RosyBrown;
                snimok_profil.BackColor = Color.RosyBrown;
                delete_one_profile.BackColor = Color.RosyBrown;
                found_profil.BackColor = Color.RosyBrown;
                save_profil.BackColor = Color.RosyBrown;
            }

        }
        public void load_settings()
        {
            string name = "settings";

            using (BinaryReader file = new BinaryReader(File.Open(name, FileMode.Open)))
            {
                try
                {
                    settings_form.checkBox1.Checked = settings.open_logs = Convert.ToBoolean(file.ReadBoolean());
                    settings_form.authorization.Checked = settings.authorization = Convert.ToBoolean(file.ReadBoolean());
                    settings.id_authorization = Convert.ToInt32(file.ReadInt32());
                    settings_form.time.SelectedIndex = Convert.ToInt32(file.ReadInt32());
                    settings_form.CB_autosave_time.SelectedIndex = Convert.ToInt32(file.ReadInt32());
                    settings_form.CB_smart_save.Checked = settings.smart_save = Convert.ToBoolean(file.ReadBoolean());
                }
                catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
            }
        }
        public void save_setting()
        {
            string name = "settings";

            using (BinaryWriter file = new BinaryWriter(File.Open(name, FileMode.Create)))
            {
                try
                {
                    file.Write(Convert.ToBoolean(settings_form.checkBox1.Checked));
                    file.Write(Convert.ToBoolean(settings.authorization));
                    file.Write(Convert.ToInt32(settings.id_authorization));
                    file.Write(Convert.ToInt32(settings_form.time.SelectedIndex));
                    file.Write(Convert.ToInt32(settings_form.CB_autosave_time.SelectedIndex));
                    file.Write(Convert.ToInt32(settings_form.CB_smart_save.Checked));

                }
                catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
            }

            load_settings();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void delete_one_profile_Click(object sender, EventArgs e)
        {
            delete_profil();
        }
        public async void delete_profil()
        {
            if (global_id_Data != null && global_rowindex_Data != -1)
            {
                if (MessageBox.Show("удалить профиль " + dataGridView1[0, global_rowindex_Data].Value + " " + dataGridView1[1, global_rowindex_Data].Value + " (id: " + dataGridView1[4, global_rowindex_Data].Value + ") из базы данных?", "удаление профиля", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string query = "DELETE FROM профиль WHERE [id_профиль] = " + global_id_Data;
                    OleDbCommand command = new OleDbCommand(query, connection_DB);
                    command.ExecuteNonQuery();
                    clear_method();
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);

                    dataGridView1.ClearSelection();
                    global_id_Data = null; global_rowindex_Data = -1;
                    work_button(false, true);
                    delete_one_profile.Enabled = false;
                    TB_focus();
                    MessageBox.Show("профиль успешно удалён", "успех");
                }
            }
            else
                MessageBox.Show("не выбран профиль для удаления", "уведомление");
        }

        private readonly HttpClient client = new HttpClient();
        private string ACCESS_TOKEN = "4382afdb4382afdb4382afdb2743ff84ad443824382afdb213432dcce17e11796951dca";
        private string DEF_KEY = "Lj67z6Jl2h8qxjEFUwva";
        private string Version = "5.131";
        string id_user = null;

        public async void method_NewtonsoftJson(bool flag) //flag - true = dgv; false = textbox
        {
            try
            {
                double time_second = DateTime.Now.Second;
                double time_milisecond = DateTime.Now.Millisecond;

                work_all_button(false);
                toolStripProgressBar1.Value = 0;

                string local_user_id;
                if (flag) local_user_id = Convert.ToString(dataGridView1[4, global_rowindex_Data].Value); else local_user_id = Convert.ToString(find_id.Text);

                //ид, фамилия, имя, день рождения, статус, город, сайт, телефон,  данные (фото, видео, подписчики, группы, посты)
                HttpResponseMessage response_users = await client.GetAsync("https://api.vk.com/method/users.get?&user_id=" + local_user_id + "&lang=0&fields=bdate,status,city,site,contacts,counters,domain,photo_max_orig,relation,education,home_town,last_seen,sex,is_favorite&access_token=" + ACCESS_TOKEN + "&v=" + Version);

                var data_user = await response_users.Content.ReadAsStringAsync();

                VK_json_users.Rootobject user = JsonConvert.DeserializeObject<VK_json_users.Rootobject>(data_user);

                if (user.response[0].first_name != "DELETED")
                {
                    pictureBox1.LoadAsync(user.response[0].photo_max_orig);

                    name.TextInput = user.response[0].first_name + " " + user.response[0].last_name;
                    status.Text = (user.response[0].status == "") ? "" : user.response[0].status;
                    bdate.Text = (user.response[0].bdate == null) ? "" : user.response[0].bdate;

                    if (user.response[0].city != null)
                        city.Text = user.response[0].city.title + " " + user.response[0].home_town;
                    else
                        city.Text = (user.response[0].home_town == "") ? "" : user.response[0].home_town;

                    education.Text = (user.response[0].university_name == null || user.response[0].university_name == "") ? "" : user.response[0].university_name + user.response[0].name;
                    site.Text = (user.response[0].site == "") ? "" : user.response[0].site;

                    contacts.Text = (user.response[0].mobile_phone == "") ? "" : (user.response[0].home_phone == null) ? "" : user.response[0].mobile_phone + " " + user.response[0].home_phone;

                    switch (user.response[0].relation)
                    {
                        case 0: relation.Text = ""; break;
                        case 1: relation.Text = "не женат/не замужем"; break;
                        case 2: relation.Text = "есть друг/есть подруга"; break;
                        case 3: relation.Text = "помолвлен/помолвлена"; break;
                        case 4: relation.Text = "женат/замужем"; break;
                        case 5: relation.Text = "всё сложно"; break;
                        case 6: relation.Text = "в активном поиске"; break;
                        case 7: relation.Text = "влюблён/влюблена"; break;
                        case 8: relation.Text = "в гражданском браке"; break;
                    }
                    if (user.response[0].relation_partner != null)
                        relation.Text += " " + user.response[0].relation_partner.last_name + " " + user.response[0].relation_partner.first_name;

                    domain.Text = "https://vk.com/" + Convert.ToString(user.response[0].domain);
                    id_user = Convert.ToString(user.response[0].id);
                    id.Text = "id: " + id_user;

                    switch (user.response[0].sex)
                    {
                        case 0: sex.Text = "не указан"; break;
                        case 1: sex.Text = "женский"; break;
                        case 2: sex.Text = "мужской"; break;
                    }

                    if (user.response[0].counters != null)
                    {
                        photos.Text = Convert.ToString(user.response[0].counters.photos);
                        videos.Text = Convert.ToString(user.response[0].counters.videos);
                        audios.Text = Convert.ToString(user.response[0].counters.audios);
                        gifts.Text = Convert.ToString(user.response[0].counters.gifts);
                        pages.Text = Convert.ToString(user.response[0].counters.pages);
                        TBfollowers.Text = Convert.ToString(user.response[0].counters.followers);
                    }
                    posts.Text = Convert.ToString(user.response[0].posts);

                    toolStripProgressBar1.Value = 60;

                    if (user.response[0].last_seen != null)
                    {
                        DateTime online_time = new DateTime(1970, 1, 1).AddSeconds(user.response[0].last_seen.time + 10800 + 3600 * settings_form.time.SelectedIndex);

                        if (Convert.ToString(sex.Text) == "женский")
                            online.Text = "была онлайн " + Convert.ToString(online_time) + " (" + settings_form.time.SelectedItem + ")";
                        else
                            online.Text = "был онлайн " + Convert.ToString(online_time) + " (" + settings_form.time.SelectedItem + ")";
                    }
                    else
                        if (Convert.ToString(sex.Text) == "женский")
                        online.Text = "была недавно (скрыто)";
                    else
                        online.Text = "был недавно (скрыто)";

                    if (global_save_flag)
                    {
                        textBox1.Text = "0";
                        save_DB(user.response[0].id, user.response[0].first_name, user.response[0].last_name, sex.Text, user.response[0].domain);
                        dataGridView1.ClearSelection();
                        global_save_flag = false;
                    }

                    toolStripProgressBar1.Value = 80;

                    //кол-во друзей
                    HttpResponseMessage response_friends = await client.GetAsync("https://api.vk.com/method/friends.get?&user_id=" + user.response[0].id + "&order=name&count=1000&fields=domain&access_token=" + ACCESS_TOKEN + "&v=" + Version);
                    var data_friends = await response_friends.Content.ReadAsStringAsync();

                    VK_json_friends.Rootobject friends = JsonConvert.DeserializeObject<VK_json_friends.Rootobject>(data_friends);

                    if (!user.response[0].is_closed & friends.response != null)
                        TBfriends.Text = Convert.ToString(friends.response.count);
                    else TBfriends.Text = "0";

                    Thread.Sleep(500);
                    last_update.Text = "последнее обновление: " + DateTime.Now.ToString();
                    TB_focus();
                    work_all_button(true);

                    if (user.response[0].is_closed)
                    {
                        type_profil.Text = "закрытый";
                        type_profil.BackColor = Color.IndianRed;

                        if (global_found_profil || global_found_spisok)
                        {
                            global_found_profil = false;
                            global_found_spisok = false;
                            work_button(false, true);
                        }
                        else work_button(true, true);
                    }
                    else
                    {
                        type_profil.Text = "открытый";
                        type_profil.BackColor = Color.Chartreuse;

                        if (global_found_profil || global_found_spisok)
                        {
                            global_found_profil = false;
                            global_found_spisok = false;
                            work_button(false, false);
                        }
                        else work_button(true, false);
                    }

                    toolStripProgressBar1.Value = 100;
                    TB_otclick.Text = (Math.Abs(DateTime.Now.Second - time_second) <= 15) ? Convert.ToString("отклик: " + (Math.Abs(DateTime.Now.Second - time_second)) + ":" + (Math.Abs(DateTime.Now.Millisecond - time_milisecond))) : "отклик: -";

                }
                else
                {
                    global_save_flag = false;
                    MessageBox.Show("такого пользователя не существует", "ошибка");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                global_save_flag = false;
                MessageBox.Show("пользователь с указанным id не найден", "ошибка");
                work_all_button(true);
            }
            catch (Exception E)
            {
                global_save_flag = false;
                MessageBox.Show(E.Message, "ошибка");
            }
        }
        public string[,] get_photos(string data)
        {
            VK_json_photos.Rootobject photo = JsonConvert.DeserializeObject<VK_json_photos.Rootobject>(data);

            string[,] array = new string[photo.response.count, 4];
            int j = 0;

            for (int i = photo.response.count - 1; i >= 0; i--)
            {
                array[j, 0] = photo.response.items[i].sizes[4].url;
                array[j, 1] = Convert.ToString(photo.response.items[i].likes.count);
                array[j, 2] = Convert.ToString(photo.response.items[i].comments.count);
                array[j, 3] = Convert.ToString(photo.response.items[i].reposts.count);
                j++;
            }

            return array;
        }
        public async void photos_catalog()
        {
            try
            {
                string info_text = string.Empty;
                string param_profil = string.Empty;
                string param_wall = string.Empty;
                string param_saved = string.Empty;
                string method = string.Empty;
                int photo_sum = 0;
                info_text = "фотографий";

                view_photos_form view_photos_form = new view_photos_form();

                string[] array_param = { "&lang=0&album_id=profile&rev=0&extended=1&count=1000&feed_type=photo&access_token=", "&lang=0&album_id=wall&rev=0&extended=1&count=1000&feed_type=photo&access_token=", "&lang=0&album_id=saved&rev=0&extended=1&count=1000&feed_type=photo&access_token=" };

                for (int c = 0; c < 3; c++)
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync("https://api.vk.com/method/photos.get?&user_id=" + id_user + array_param[c] + ACCESS_TOKEN + "&v=" + Version);
                    var data = await response.Content.ReadAsStringAsync();

                    VK_json_photos.Rootobject photo = JsonConvert.DeserializeObject<VK_json_photos.Rootobject>(data);

                    if (photo.response != null && photo.response.count != 0)
                    {
                        if (c == 0)
                            view_photos_form.url_profile = get_photos(data);
                        if (c == 1)
                            view_photos_form.url_wall = get_photos(data);
                        if (c == 2)
                            view_photos_form.url_saved = get_photos(data);

                        photo_sum += photo.response.count;
                    }
                }
                string[] word = name.Text.Split();
                view_photos_form.Text = "список " + info_text + " " + word[0] + " " + word[1] + "    кол-во " + info_text + ": " + photo_sum;

                if (view_photos_form.ShowDialog() == DialogResult.Cancel)
                    this.Focus();

            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
        }

        public string[,] get_people(string data)
        {
            VK_json_friends.Rootobject spisok = JsonConvert.DeserializeObject<VK_json_friends.Rootobject>(data);

            int lenght = 999;
            if (spisok.response.count < 1000) lenght = spisok.response.count;

            string[,] array = new string[7, lenght];

            for (int i = 0; i < lenght; i++)
            {
                array[0, i] = Convert.ToString(i + 1);
                array[1, i] = Convert.ToString(spisok.response.items[i].id);
                array[2, i] = Convert.ToString(spisok.response.items[i].last_name);
                array[3, i] = Convert.ToString(spisok.response.items[i].first_name);

                switch (spisok.response.items[i].sex)
                {
                    case 0: array[4, i] = Convert.ToString("не указан"); break;
                    case 1: array[4, i] = Convert.ToString("женский"); break;
                    case 2: array[4, i] = Convert.ToString("мужской"); break;
                }

                if (spisok.response.items[i].city != null)
                    array[5, i] = Convert.ToString(spisok.response.items[i].city.title);
                else
                    array[5, i] = Convert.ToString("-");

                array[6, i] = Convert.ToString(spisok.response.items[i].domain);
            }

            return array;
        }

        public async void piople_catalogAsync(string name_catalog)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                string info_text = string.Empty;
                string param = string.Empty;
                string method = string.Empty;

                if (name_catalog == "followers")
                {
                    method = "users.getFollowers";
                    param = "&lang=0&count=1000&fields=domain,sex,city&access_token=";
                    info_text = "подписчиков";
                }
                else
                if (name_catalog == "friends")
                {
                    method = "friends.get";
                    param = "&lang=0&order=name&count=5000&fields=domain,sex,city&access_token=";
                    info_text = "друзей";
                }

                response = await client.GetAsync("https://api.vk.com/method/" + method + "?&user_id=" + id_user + param + ACCESS_TOKEN + "&v=" + Version);
                var data = await response.Content.ReadAsStringAsync();

                VK_json_friends.Rootobject spisok = JsonConvert.DeserializeObject<VK_json_friends.Rootobject>(data);

                spisok_form spisok_form = new spisok_form();
                string[] word = name.Text.Split();
                spisok_form.Text = "список " + info_text + " " + word[0] + " " + word[1] + "; кол-во " + info_text + ": " + spisok.response.count;

                spisok_form.main_spisik = get_people(data);

                if (spisok_form.ShowDialog() == DialogResult.OK && (spisok_form.global_rowindex_Data != -1))
                {
                    clear_method();
                    dataGridView1.ClearSelection();
                    find_id.Text = Convert.ToString(spisok_form.dataGridView1[1, spisok_form.global_rowindex_Data].Value);
                    found_or_save_profil(false);
                    delete_one_profile.Enabled = false;
                }
                else
                    this.Focus();
            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }

        }
        private void open_spisok_friends_Click_1(object sender, EventArgs e)
        {
            piople_catalogAsync("friends");
        }

        private void open_spisok_followers_Click_1(object sender, EventArgs e)
        {
            piople_catalogAsync("followers");

        }
        private void open_view_photo_Click_1(object sender, EventArgs e)
        {
            photos_catalog();
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            try { Clipboard.SetText(Convert.ToString(domain.Text)); } catch (Exception) { }
        }
        private void button9_Click_1(object sender, EventArgs e)
        {
            try { Clipboard.SetText(Convert.ToString(id_user)); } catch (Exception) { }
        }
        private void paste_Click(object sender, EventArgs e)
        {
            find_id.Text = Clipboard.GetText();
            find_id.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection_DB.Close();
        }

        private async void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.White;
                delete_one_profile.Enabled = true; //удалить выбранный профиль

                RB_autosnimok_time.Checked = false; // отключить CB автоснимки онлайна

                global_id_Data = Convert.ToString(dataGridView1[5, e.RowIndex].Value);
                global_rowindex_Data = e.RowIndex;

                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT друзья, подписчики, фотографий, видеозаписей, аудиозаписей, сообществ, подарки, дата FROM снимок WHERE id_профиль=" + dataGridView1[5, global_rowindex_Data].Value, connection_DB);
                DataTable table = new DataTable();
                adapter.Fill(table);
                textBox1.Text = Convert.ToString(table.Rows.Count);

                if (dataGridView1[5, e.RowIndex].Value != "")
                    method_NewtonsoftJson(true);
            }
            catch (Exception E) { /*MessageBox.Show(E.Message, "ошибка"); */};

        }

        private void open_form_option()
        {
            if (settings_form.ShowDialog() == DialogResult.OK)
            {
                settings.authorization = settings_form.authorization.Checked;
                save_setting();
                MessageBox.Show("настройки успешно изменены", "успех");
            }
        }

        settings_form settings_form = new settings_form();
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open_form_option();
        }

        private void snimok_profil_Click_1(object sender, EventArgs e)
        {
            create_snimok();
        }

        private async void create_snimok()
        {
            try
            {
                if (global_rowindex_Data != -1)
                {
                    string query = "INSERT INTO снимок (id_user, друзья, подписчики, фотографий, видеозаписей, аудиозаписей, сообществ, подарки, статус, семейное_положение, место_учебы, сайт, город, день_рождения, номер_телефона, nickname, дата, id_профиль) VALUES ('" + dataGridView1[4, global_rowindex_Data].Value + "','" + TBfriends.Text + "','" + TBfollowers.Text + "','" + photos.Text + "','" + videos.Text + "','" + audios.Text + "','" + pages.Text + "','" + gifts.Text + "','" + status.Text + "','" + relation.Text + "','" + education.Text + "','" + site.Text + "','" + city.Text + "','" + bdate.Text + "','" + contacts.Text + "','" + domain.Text.Replace("https://vk.com/", "") + "','" + DateTime.Now.ToLongDateString() + "','" + dataGridView1[5, global_rowindex_Data].Value + "')";
                    OleDbCommand command = new OleDbCommand(query, connection_DB);
                    command.ExecuteNonQuery();

                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT id_снимок, id_user, id_профиль FROM снимок WHERE id_user =" + dataGridView1[4, global_rowindex_Data].Value, connection_DB);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync("https://api.vk.com/method/friends.get?&user_id=" + id_user + "&lang=0&order=name&count=5000&fields=domain,sex,city&access_token=" + ACCESS_TOKEN + "&v=" + Version);
                    var data = await response.Content.ReadAsStringAsync();

                    string[,] people = get_people(data);
                    for (int i = 0; i < people.GetLength(1); i++)
                    {
                        query = @"INSERT INTO список_друзей (id_снимок, id_user, фамилия, имя, пол, город, nickname, id_профиль) VALUES ('" + table.Rows[table.Rows.Count - 1]["id_снимок"] + "','" + people[1, i] + "','" + people[2, i].Replace("'", "") + "','" + people[3, i].Replace("'", "") + "','" + people[4, i] + "','" + people[5, i] + "','" + people[6, i] + "','" + dataGridView1[5, global_rowindex_Data].Value + "')";
                        command = new OleDbCommand(query, connection_DB);
                        command.ExecuteNonQuery();
                    }

                    response = new HttpResponseMessage();
                    response = await client.GetAsync("https://api.vk.com/method/users.getFollowers?&user_id=" + id_user + "&lang=0&count=1000&fields=domain,sex,city&access_token=" + ACCESS_TOKEN + "&v=" + Version);
                    data = await response.Content.ReadAsStringAsync();

                    people = get_people(data);

                    for (int i = 0; i < people.GetLength(1); i++)
                    {
                        query = @"INSERT INTO список_подписчиков (id_снимок, id_user, фамилия, имя, пол, город, nickname, id_профиль) VALUES ('" + table.Rows[table.Rows.Count - 1]["id_снимок"] + "','" + people[1, i] + "','" + people[2, i].Replace("'", "") + "','" + people[3, i].Replace("'", "") + "','" + people[4, i] + "','" + people[5, i] + "','" + people[6, i] + "','" + dataGridView1[5, global_rowindex_Data].Value + "')";
                        command = new OleDbCommand(query, connection_DB);
                        command.ExecuteNonQuery();
                    }

                    textBox1.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) + 1);

                    MessageBox.Show("снимок успешно сохранён в базу данных", "успех");
                }
                else
                    MessageBox.Show("сохраните и выберите профиль", "уведомление");

            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
        }

        private void tendencii_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (global_rowindex_Data != -1)
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT друзья, подписчики, фотографий, видеозаписей, аудиозаписей, сообществ, подарки, дата FROM снимок WHERE id_профиль=" + dataGridView1[5, global_rowindex_Data].Value, connection_DB);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if (table.Rows.Count > 1)
                    {
                        textBox1.Text = Convert.ToString(table.Rows.Count);
                        trends_form form = new trends_form();
                        form.string_leight(table.Rows.Count);

                        form.global_id_Data = global_id_Data;
                        form.Text = "тенденции пользователя " + name.Text;

                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            form.array_friends[i] = Convert.ToInt32(table.Rows[i]["друзья"]);
                            form.array_followers[i] = Convert.ToInt32(table.Rows[i]["подписчики"]);
                            form.array_photos[i] = Convert.ToInt32(table.Rows[i]["фотографий"]);
                            form.array_videos[i] = Convert.ToInt32(table.Rows[i]["видеозаписей"]);
                            form.array_audios[i] = Convert.ToInt32(table.Rows[i]["аудиозаписей"]);
                            form.array_pades[i] = Convert.ToInt32(table.Rows[i]["сообществ"]);
                            form.array_gifts[i] = Convert.ToInt32(table.Rows[i]["подарки"]);
                            form.array_date[i] = Convert.ToString(table.Rows[i]["дата"]);
                        }

                        adapter = new OleDbDataAdapter("SELECT дата, статус, день_рождения, город, место_учебы, сайт, семейное_положение, номер_телефона, nickname FROM снимок WHERE id_профиль=" + dataGridView1[5, global_rowindex_Data].Value, connection_DB);
                        table = new DataTable();
                        adapter.Fill(table);

                        string[] array = { "дата", "статус", "день_рождения", "город", "место_учебы", "сайт", "семейное_положение", "номер_телефона", "nickname" };

                        for (int i = 0; i < table.Rows.Count; i++)
                            for (int j = 0; j < array.Length; j++)
                                form.dop_info[j, i] = Convert.ToString(table.Rows[i][array[j]]);

                        form.Show();
                    }
                    else
                        MessageBox.Show("графики можно анализировать только с сохраненными пользователями, имеющими 2 и более снимка", "уведомление");
                }
                else
                    MessageBox.Show("сохраните и выберите профиль", "уведомление");
            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
        }

        public string temp_time = null;
        public string last_online = null;
        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime c = DateTime.Now;              

            if (RB_autosnimok_time.Checked && global_rowindex_Data != -1)
            {
                if (temp_time == c.ToLongTimeString() || temp_time == null)
                {
                    DateTime q = DateTime.Now;

                    method_NewtonsoftJson(true);
                    Thread.Sleep(3000); //задержка, чтобы все успело обновиться

                    switch (settings_form.CB_autosave_time.SelectedIndex)
                    {
                        case 0:
                            q = DateTime.Now.AddSeconds(30); //30 сек
                            break;
                        case 1:
                            q = DateTime.Now.AddSeconds(60);//1 мин
                            break;
                        case 2:
                            q = DateTime.Now.AddSeconds(180);//3 мин
                            break;
                        case 3:
                            q = DateTime.Now.AddSeconds(300);//5 мин
                            break;
                        case 4:
                            q = DateTime.Now.AddSeconds(600);//10 мин
                            break;
                        case 5:
                            q = DateTime.Now.AddSeconds(1800);//30 мин
                            break;
                        case 6:
                            q = DateTime.Now.AddSeconds(3600);//60 мин
                            break;
                        case 7:
                            q = DateTime.Now.AddSeconds(21600);//12 час
                            break;
                        case 8:
                            q = DateTime.Now.AddSeconds(23200);//1 день
                            break;
                        case 9:
                            q = DateTime.Now.AddSeconds(10);//10 сек
                            break;
                    }

                        temp_time = Convert.ToString(q.ToLongTimeString());

                        if (last_online != online.Text || last_online == null)
                        {
                            last_online = online.Text;
                            autosave_time(c.ToLongTimeString(), c.ToShortDateString());
                        }
                        else
                            if (!settings.smart_save)
                            autosave_time(c.ToLongTimeString(), c.ToShortDateString());
                }
            }
            else
            {
                temp_time = null;
                if (RB_autosnimok_time.Checked && global_rowindex_Data == -1)
                {
                    RB_autosnimok_time.Checked = false;
                    last_online = null;
                    MessageBox.Show("не выбран профиль для автоматического сохранения");
                }
            }

            //last_update.Text = c.ToShortDateString();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (settings_form.ShowDialog() == DialogResult.OK)
            {
                //settings.authorization = settings_form.authorization.Checked;
                //settings.authorization = settings_form.authorization.Checked;
                RB_autosnimok_time.Checked = false;
                save_setting();
                MessageBox.Show("настройки успешно изменены", "успех");
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            open_form_option();
        }

        private void abauot_programm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("главной целью VK_Parser by FallGen является сбор, хранение, мониторинг, анализ, построение тенденций и удобное предоставление информации профилей пользователей сервиса социальной сети Вконтакте посредством VK API. хранение данных осуществляется непосредственно в локальной реляционной СУБД microsoft accsess\n \n автор: гусельников виталий александрович \n связь: @fallgen", "о приложении");
        }

        private void autosave_time(string time, string date)
        {
            DateTime c = DateTime.Now;

            try
            {
                if (global_rowindex_Data != -1)
                {
                    string query = @"INSERT INTO мониторинг_онлайна (id_профиль, имя_фамилия, последний_онлайн, время_снимка, дата_снимка) VALUES ('" + dataGridView1[5, global_rowindex_Data].Value + "','" + name.Text + "','" + online.Text + "','" + time + "','" + date + "')";
                    OleDbCommand command = new OleDbCommand(query, connection_DB);
                    command.ExecuteNonQuery();
                }

                last_update.Text = "автоматическое сохранение онлайна " + c.ToLongTimeString();
            }
            catch
            {

            }
        }
    }
}