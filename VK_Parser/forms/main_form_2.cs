using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using yt_DesignUI;

namespace VK_Parser.forms
{
    public partial class main_form_2 : Form
    {

        //подключение бд 
        public static string way_DB = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = Data_VK_parser.accdb";
        private OleDbConnection connection_DB;

        //инициализация глобальных переменных
        bool global_save_flag = false;
        string global_id_Data = string.Empty;
        int global_rowindex_Data = -1;
        bool global_found_spisok = false;
        bool global_found_profil = false;

        //инициализация классов и форм
        settings settings = new settings();
        settings_form settings_form = new settings_form();

        public main_form_2()
        {
            InitializeComponent();
        }
        private void main_form_2_Load(object sender, EventArgs e)
        {
            try
            {
                Animator.Start();
                connection_DB = new OleDbConnection(way_DB);
                connection_DB.Open();
            }
            catch
            {
                MessageBox.Show("ошибка загрузки анимаций: перезапустите приложение", "уведомление");
            }

            comboBox4.SelectedIndex = 0;
            visible_icon_analiz(true);

            load_settings();
            authorization();
            adapterReader("SELECT id_user, id_авторизация, фамилия, имя, пол, nickname, id_профиль FROM профиль WHERE id_авторизация=" + settings.id_authorization, "main_dgv");
            dataGridView1.ClearSelection();
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

                update_value_snimok();

                dataGridView1.Refresh();
            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
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
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.White;
                delete_one_profile.Enabled = true; //удалить выбранный профиль
                delete_one_profile.BackColor = Color.DeepSkyBlue;
                //RB_autosnimok_time.Checked = false; // отключить CB автоснимки онлайна

                //save_profil.BackColor = Color.SteelBlue;
                //save_profil.Enabled = false;

                global_id_Data = Convert.ToString(dataGridView1[5, e.RowIndex].Value);
                global_rowindex_Data = e.RowIndex;

                clear_method();

                if (dataGridView1[5, e.RowIndex].Value != "")
                    method_NewtonsoftJson(true);
            }
            catch (Exception E) { /*MessageBox.Show(E.Message, "ошибка"); */};
        }
        private void find_id_TextChanged(object sender, EventArgs e)
        {
            if (find_id.Text.Trim() != "")
            {
                save_profil.BackColor = Color.DeepSkyBlue;
                save_profil.Enabled = true;
                found_profil.BackColor = Color.DeepSkyBlue;
                found_profil.Enabled = true;
            }
            else
            {
                save_profil.BackColor = Color.SteelBlue;
                save_profil.Enabled = false;
                found_profil.BackColor = Color.SteelBlue;
                found_profil.Enabled = false;
            }
        }


        private readonly HttpClient client = new HttpClient();
        private string ACCESS_TOKEN = "4382afdb4382afdb4382afdb2743ff84ad443824382afdb213432dcce17e11796951dca";
        //private string DEF_KEY = "Lj67z6Jl2h8qxjEFUwva";
        private string Version = "5.131";
        string id_user = null;

        public async void method_NewtonsoftJson(bool flag) //flag - true = dgv; false = textbox
        {
            try
            {
                double time_second = DateTime.Now.Second;
                double time_milisecond = DateTime.Now.Millisecond;

                //work_all_button(false);
                //toolStripProgressBar1.Value = 0;

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
                    name1.TextInput = user.response[0].first_name + " " + user.response[0].last_name;
                    status.Text = (user.response[0].status == "") ? "" : user.response[0].status;
                    bdate.Text = (user.response[0].bdate == null) ? "" : user.response[0].bdate;

                    this.Text = "VK_Parser by FallGen: профиль " + name.Text;
                    //main_form_2.ActiveForm.Text = "VK_Parser by FallGen: профиль " + name.Text;

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
                    //posts.Text = Convert.ToString(user.response[0].posts);

                    //toolStripProgressBar1.Value = 60;

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

                    // toolStripProgressBar1.Value = 80;

                    //кол-во друзей
                    HttpResponseMessage response_friends = await client.GetAsync("https://api.vk.com/method/friends.get?&user_id=" + user.response[0].id + "&order=name&count=1000&fields=domain&access_token=" + ACCESS_TOKEN + "&v=" + Version);
                    var data_friends = await response_friends.Content.ReadAsStringAsync();

                    VK_json_friends.Rootobject friends = JsonConvert.DeserializeObject<VK_json_friends.Rootobject>(data_friends);

                    if (!user.response[0].is_closed & friends.response != null)
                        TBfriends.Text = Convert.ToString(friends.response.count);
                    else TBfriends.Text = "0";

                    //Thread.Sleep(500);
                    last_update.Text = "последнее обновление: " + DateTime.Now.ToString();
                    TB_focus();
                    //work_all_button(true);

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

                    //toolStripProgressBar1.Value = 100;
                    //TB_otclick.Text = (Math.Abs(DateTime.Now.Second - time_second) <= 15) ? Convert.ToString("отклик: " + (Math.Abs(DateTime.Now.Second - time_second)) + ":" + (Math.Abs(DateTime.Now.Millisecond - time_milisecond))) : "отклик: -";

                    load_info();

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
                //work_all_button(true);
            }
            catch (Exception E)
            {
                global_save_flag = false;
                MessageBox.Show(E.Message, "ошибка");
            }
        }
        public void load_info()
        {
            photos_catalog();
            piople_catalogAsync("friends");
            Thread.Sleep(1000);
            piople_catalogAsync("followers");

            if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
                load_profile();
        }
        private async void save_DB(int id, string first_name, string last_name, string sex, string domain)
        {
            string query = "INSERT INTO профиль (id_user, id_авторизация, фамилия, имя, пол, nickname) VALUES ('" + id + "','" + settings.id_authorization + "','" + last_name + "','" + first_name + "','" + sex + "','" + domain + "')";
            OleDbCommand command = new OleDbCommand(query, connection_DB);
            command.ExecuteNonQuery();

            adapterReader("SELECT id_user, id_авторизация, фамилия, имя, пол, nickname, id_профиль FROM профиль WHERE id_авторизация=" + settings.id_authorization, "main_dgv");

            MessageBox.Show("профиль успешно сохранён в базу данных", "успех");
        }
        public void work_button(bool flag, bool close_profil)
        {
            snimok_profil.Enabled = flag; // сделать снимок

            if (!flag)
            {
                //закрытие кнопок при открытом профиле
                snimok_profil.Enabled = false; // сделать снимок
                snimok_profil.BackColor = Color.SteelBlue; // сделать снимок
            }
            else
            {
                if (!close_profil)
                {
                    //открытие кнопок при закрытом профиле
                    snimok_profil.BackColor = Color.DeepSkyBlue; // сделать снимок
                }
                else
                {
                    //закрытие кнопок при открытом профиле
                    snimok_profil.BackColor = Color.SteelBlue; // сделать снимок
                }
            }

            if (close_profil)
            {
                //закрытие кнопок при открытом профиле
                snimok_profil.Enabled = false; // сделать снимок
            }
            else
            {
            }
        }
        public void TB_focus()
        {
            if (tabControl1.SelectedIndex <= 1)
            {
                type_profil.Focus();
                online.Focus();
                domain.Focus();
                id.Focus();
                textBox1.Focus();
                name.Focus();
                name1.Focus();
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
                find_id.Focus();
            }
        }
        private void found_profil_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            global_rowindex_Data = -1;
            global_found_profil = true;
            found_or_save_profil(false);
        }

        public void found_or_save_profil(bool flag)
        {
            if (find_id.Text != "")
            {
                delete_one_profile.Enabled = true;
                delete_one_profile.BackColor = Color.SteelBlue;
                clear_method();
                global_found_spisok = true;
                global_save_flag = flag;
                method_NewtonsoftJson(false);
            }
            else
            {
                MessageBox.Show("не введен id или пустой профиль", "уведомление");
            }
        }
        public void clear_method()
        {
            pictureBox1.Image = null;
            online.Text = "";
            name.Text = "";
            name1.Text = "";
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
            textBox1.Text = "";
            videos.Text = "";
            domain.Text = "";
            status.Text = "";
            sex.Text = "";
            type_profil.Text = "профиль:";
            type_profil.BackColor = SystemColors.Control;
            pictureBox2.Image = null;
            dataGridView2.RowCount = 0;
            dataGridView3.RowCount = 0;
            dataGridView4.RowCount = 0;
            spisok_friends = null;
            spisok_followers = null;
            CB_from.Text = null;
            CB_before.Text = null;
            main_spisik = null;
            CB_from.Items.Clear();
            CB_before.Items.Clear();
            CB_otobrajenie_date.Items.Clear();
            CB_sravnenie_date1.Items.Clear();
            CB_sravnenie_date2.Items.Clear();
            for (int i = 0; i <= 6; i++)
                chart1.Series[i].Points.Clear();
        }
        private void save_profil_Click(object sender, EventArgs e)
        {
            found_or_save_profil(true);
        }
        private void save_profil2_Click(object sender, EventArgs e)
        {
            found_or_save_profil(true);
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
                    delete_one_profile.BackColor = Color.SteelBlue;
                    delete_one_profile.Enabled = false;
                    main_form_2.ActiveForm.Text = "VK_Parser by FallGen";
                    TB_focus();
                    MessageBox.Show("профиль успешно удалён", "успех");
                }
            }
            else
                MessageBox.Show("не выбран профиль для удаления", "уведомление");
        }
        private void main_form_2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //безопасное закрытие базы данных
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            e.Cancel = true;
            //notifyIcon1.Visible = false;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string name_form = "пустой профиль";

            if (name.Text != string.Empty)
                name_form = name.Text;

            //блокировка кнопок "найти профиль"
            yt_Button1.Enabled = false;
            yt_Button2.Enabled = false;
            yt_Button6.Enabled = false;

            yt_Button1.BackColor = Color.SteelBlue;
            yt_Button2.BackColor = Color.SteelBlue;
            yt_Button6.BackColor = Color.SteelBlue;

            TB_focus();
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    main_form_2.ActiveForm.Text = "VK_Parser by FallGen: профиль " + name_form;
                    break;
                case 1:
                    main_form_2.ActiveForm.Text = "VK_Parser by FallGen: информация о " + name_form;
                    break;
                case 2:
                    main_form_2.ActiveForm.Text = "VK_Parser by FallGen: фотографии " + name_form;
                    break;
                case 3:
                    dataGridView2.ClearSelection();
                    main_form_2.ActiveForm.Text = "VK_Parser by FallGen: друзья " + name_form;
                    break;
                case 4:
                    dataGridView3.ClearSelection();
                    main_form_2.ActiveForm.Text = "VK_Parser by FallGen: подписчики " + name_form;
                    break;
                case 5:
                    dataGridView4.ClearSelection();
                    main_form_2.ActiveForm.Text = "VK_Parser by FallGen: анализ " + name_form;
                    break;
            }
        }
        private void snimok_profil_Click(object sender, EventArgs e)
        {
            dataGridView2.RowCount = 0;
            dataGridView3.RowCount = 0;
            dataGridView4.RowCount = 0;
            spisok_friends = null;
            spisok_followers = null;
            CB_from.Text = null;
            CB_before.Text = null;
            CB_from.Items.Clear();
            CB_before.Items.Clear();

            create_snimok();
        }
        public void update_value_snimok()
        {
            // 7 столбец: кол-во снимков
            int i = 0;
            while (i < dataGridView1.RowCount)
            {

                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT друзья, подписчики, фотографий, видеозаписей, аудиозаписей, сообществ, подарки, дата FROM снимок WHERE id_профиль=" + dataGridView1[5, i].Value, connection_DB);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1[7, i].Value = Convert.ToString(table.Rows.Count);
                i++;
            }

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

                    update_value_snimok();
                    load_info();
                    //textBox1.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) + 1);

                    MessageBox.Show("снимок успешно сохранён в базу данных", "успех");
                }
                else
                    MessageBox.Show("сохраните и выберите профиль", "уведомление");

            }
            catch (Exception E) { /*MessageBox.Show(E.Message, "ошибка");*/ }
        }


        //просмотр фотографий 
        #region
        public int value = 0;
        public int[] profile_info; // 0 - likes | 1 - comments | 2 - reposts
        public string[,] get_photos(string data)
        {
            VK_json_photos.Rootobject photo = JsonConvert.DeserializeObject<VK_json_photos.Rootobject>(data);

            string[,] array = new string[photo.response.count, 4];
            int j = 0;

            try
            {
                for (int i = photo.response.count - 1; i >= 0; i--)
                {
                    array[j, 0] = photo.response.items[i].sizes[4].url;
                    array[j, 1] = Convert.ToString(photo.response.items[i].likes.count);
                    array[j, 2] = Convert.ToString(photo.response.items[i].comments.count);
                    array[j, 3] = Convert.ToString(photo.response.items[i].reposts.count);
                    j++;
                }
            }
            catch { }

            return array;
        }

        public string[,] url_profile;
        public string[,] url_wall;
        public string[,] url_saved;

        public string[,] main_url;
        public async void photos_catalog()
        {
            try
            {
                url_profile = null;
                url_wall = null;
                url_saved = null;
                main_url = null;

                string[] array_param = { "&lang=0&album_id=profile&rev=0&extended=1&count=999&feed_type=photo&access_token=", "&lang=0&album_id=wall&rev=0&extended=1&count=999&feed_type=photo&access_token=", "&lang=0&album_id=saved&rev=0&extended=1&count=999&feed_type=photo&access_token=" };

                for (int c = 0; c < 2; c++)
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync("https://api.vk.com/method/photos.get?&user_id=" + id_user + array_param[c] + ACCESS_TOKEN + "&v=" + Version);
                    var data = await response.Content.ReadAsStringAsync();

                    VK_json_photos.Rootobject photo = JsonConvert.DeserializeObject<VK_json_photos.Rootobject>(data);

                    //if (c == 0)
                    //    url_profile = new string[photo.response.count * 4, photo.response.count * 4];
                    //if (c == 1)
                    //    url_wall = new string[photo.response.count * 4, photo.response.count * 4];
                    //if (c == 2)
                    //    url_saved = new string[photo.response.count * 4, photo.response.count * 4];


                    if (photo.response != null && photo.response.count != 0)
                    {
                        if (c == 0)
                            url_profile = get_photos(data);
                        if (c == 1)
                            url_wall = get_photos(data);
                        if (c == 2)
                            url_saved = get_photos(data);
                    }
                }

                update_form();
            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
        }

        public void update_form()
        {
            radioButton1.Checked = true;

            if (url_profile != null)
                label1.Text = Convert.ToString(url_profile.GetLength(0));
            else label1.Visible = false;

            if (url_wall != null)
                label3.Text = Convert.ToString(url_wall.GetLength(0));
            else label3.Visible = false;

            if (url_saved != null)
                label4.Text = Convert.ToString(url_saved.GetLength(0));
            else label4.Visible = false;

            main_url = url_profile;
            value = 0;
            updatePhoto();
        }
        public async void updatePhoto()
        {
            if (main_url != null)
            {
                //фотографии есть
                yt_Button3.Visible = true;
                yt_Button4.Visible = true;
                label16.Visible = false;
                pictureBox2.Visible = true;
                textBox1.Text = (value + 1) + "/" + main_url.GetLength(0);
                pictureBox2.ImageLocation = main_url[value, 0];
                label22.Text = main_url[value, 1];
                label21.Text = main_url[value, 2];
                label20.Text = main_url[value, 3];
                visible_info_photo(true);
            }
            else
            {
                //фотографий нет
                yt_Button3.Visible = false;
                yt_Button4.Visible = false;
                label16.Visible = true;
                textBox1.Clear();
                pictureBox2.Visible = false;
                visible_info_photo(false);
            }
        }

        public void visible_info_photo(bool flag)
        {
            label17.Visible = flag;
            label18.Visible = flag;
            label19.Visible = flag;
            label20.Visible = flag;
            label21.Visible = flag;
            label22.Visible = flag;
        }

        private void yt_Button4_Click(object sender, EventArgs e)
        {
            //лево
            try
            {
                if (value == 0)
                    value = main_url.GetLength(0) - 1;
                else
                    value--;
                updatePhoto();
            }
            catch { }
        }

        private void yt_Button3_Click(object sender, EventArgs e)
        {
            //право
            try
            {
                if (value == main_url.GetLength(0) - 1)
                    value = 0;
                else
                    value++;
                updatePhoto();
            }
            catch { }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //профиль
            main_url = null;
            main_url = url_profile;
            value = 0;
            updatePhoto();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //стена
            main_url = null;
            main_url = url_wall;
            value = 0;
            updatePhoto();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //сохры
            main_url = null;
            main_url = url_saved;
            value = 0;
            updatePhoto();
        }

        #endregion


        //просмотр друзей и подписчиков 
        #region
        //public string[,] main_spisik;
        public string[,] spisok_friends;
        public string[,] spisok_followers;
        //public int local_lenght;
        public string[,] get_people(string data)
        {
            VK_json_friends.Rootobject spisok = JsonConvert.DeserializeObject<VK_json_friends.Rootobject>(data);

            int lenght = 999;
            //if (spisok.response != null)
            if (spisok.response.count < 1000)
                lenght = spisok.response.count;

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
                if (type_profil.Text == "открытый")
                {
                    HttpResponseMessage response = new HttpResponseMessage();

                    //string info_text = string.Empty;
                    string param = string.Empty;
                    string method = string.Empty;

                    if (name_catalog == "followers")
                    {
                        method = "users.getFollowers";
                        param = "&lang=0&count=1000&fields=domain,sex,city&access_token=";
                        //info_text = "подписчиков";
                        response = await client.GetAsync("https://api.vk.com/method/" + method + "?&user_id=" + id_user + param + ACCESS_TOKEN + "&v=" + Version);
                    }
                    else
                    if (name_catalog == "friends")
                    {
                        method = "friends.get";
                        param = "&lang=0&order=name&count=5000&fields=domain,sex,city&access_token=";
                        //info_text = "друзей";
                        response = await client.GetAsync("https://api.vk.com/method/" + method + "?&user_id=" + id_user + param + ACCESS_TOKEN + "&v=" + Version);
                    }

                    var data = await response.Content.ReadAsStringAsync();

                    //VK_json_friends.Rootobject spisok = JsonConvert.DeserializeObject<VK_json_friends.Rootobject>(data);
                    //main_spisik = get_people(data);

                    if (name_catalog == "friends")
                    {
                        spisok_friends = get_people(data);
                        update_list_freinds();
                    }
                    else
                    {
                        spisok_followers = get_people(data);
                        update_list_followers();
                    }

                    visible_info_spiski(true);
                }
                else
                {
                    visible_info_spiski(false);
                }
            }

            catch (Exception E) { /*MessageBox.Show(E.Message, "ошибка111");*/ }
        }
        public int result_people(int lenght, string catalog)
        {
            int value_girl = 0;

            string[,] local_spisok = null;

            if (catalog == "friends")
                local_spisok = spisok_friends;
            else
                local_spisok = spisok_followers;

            for (int i = 0; i < lenght; i++)
                if (local_spisok[4, i] == "женский") value_girl++;

            return value_girl;
        }
        public void update_list_freinds()
        {
            //dataGridView2.RowCount = 0;
            for (int i = 0; i < spisok_friends.GetLength(1); i++)
            {
                dataGridView2.RowCount++;
                for (int j = 0; j < spisok_friends.GetLength(0); j++)
                    dataGridView2[j, i].Value = Convert.ToString(spisok_friends[j, i]);
            }

            label5.Text = Convert.ToString(dataGridView2.Rows.Count);
            label6.Text = Convert.ToString(result_people(dataGridView2.Rows.Count, "friends"));
            label7.Text = Convert.ToString(dataGridView2.Rows.Count - Convert.ToInt32(label6.Text));

            dataGridView2.Refresh();

            //local_lenght = main_spisik.GetLength(1);
        }
        public void update_list_followers()
        {
            dataGridView3.RowCount = 0;
            for (int i = 0; i < spisok_followers.GetLength(1); i++)
            {
                dataGridView3.RowCount++;
                for (int j = 0; j < spisok_followers.GetLength(0); j++)
                    dataGridView3[j, i].Value = Convert.ToString(spisok_followers[j, i]);
            }

            label10.Text = Convert.ToString(dataGridView3.Rows.Count);
            label11.Text = Convert.ToString(result_people(dataGridView3.Rows.Count, "followers"));
            label12.Text = Convert.ToString(dataGridView3.Rows.Count - Convert.ToInt32(label11.Text));

            dataGridView3.Refresh();

            //local_lenght = main_spisik.GetLength(1);
        }
        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView2.Rows[e.RowIndex].Selected = true;
                yt_Button1.BackColor = Color.DeepSkyBlue;
                yt_Button1.Enabled = true;
                dataGridView2.Rows[e.RowIndex].Selected = true;
                dataGridView2.RowsDefaultCellStyle.SelectionForeColor = Color.White;

                global_rowindex_Data = e.RowIndex;
            }
            catch (Exception E) { }

        }
        private void egoldsGoogleTextBox1_TextChanged(object sender, EventArgs e)
        {

            dataGridView2.RowCount = 0;
            int c = 0;
            string text = TB_find_friends.Text.ToLower();

            for (int i = 0; i < spisok_friends.GetLength(1); i++)
            {
                if (spisok_friends[1, i].ToLower().StartsWith(text) || spisok_friends[2, i].ToLower().StartsWith(text) || spisok_friends[3, i].ToLower().StartsWith(text) || spisok_friends[5, i].ToLower().StartsWith(text) || spisok_friends[6, i].ToLower().StartsWith(text))
                {
                    dataGridView2.RowCount++;

                    for (int j = 0; j < spisok_friends.GetLength(0); j++)
                        dataGridView2[j, c].Value = Convert.ToString(spisok_friends[j, i]);
                    c++;
                }
            }
        }
        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView3.Rows[e.RowIndex].Selected = true;
                yt_Button2.BackColor = Color.DeepSkyBlue;
                yt_Button2.Enabled = true;
                dataGridView3.Rows[e.RowIndex].Selected = true;
                dataGridView3.RowsDefaultCellStyle.SelectionForeColor = Color.White;

                global_rowindex_Data = e.RowIndex;

            }
            catch (Exception E) { }

        }
        private void TB_find_followers_TextChanged(object sender, EventArgs e)
        {
            dataGridView3.RowCount = 0;
            int c = 0;
            string text = TB_find_followers.Text.ToLower();

            for (int i = 0; i < spisok_followers.GetLength(1); i++)
            {
                if (spisok_followers[1, i].ToLower().StartsWith(text) || spisok_followers[2, i].ToLower().StartsWith(text) || spisok_followers[3, i].ToLower().StartsWith(text) || spisok_followers[5, i].ToLower().StartsWith(text) || spisok_followers[6, i].ToLower().StartsWith(text))
                {
                    dataGridView3.RowCount++;

                    for (int j = 0; j < spisok_followers.GetLength(0); j++)
                        dataGridView3[j, c].Value = Convert.ToString(spisok_followers[j, i]);
                    c++;
                }
            }
        }
        public void find_profile(string flag)
        {
            if (flag == "friends")
                find_id.Text = Convert.ToString(dataGridView2[1, global_rowindex_Data].Value);
            else if (flag == "followers")
                find_id.Text = Convert.ToString(dataGridView3[1, global_rowindex_Data].Value);
            else
                find_id.Text = Convert.ToString(dataGridView4[1, global_rowindex_Data].Value);

            clear_method();

            found_or_save_profil(false);
            delete_one_profile.Enabled = false;
            delete_one_profile.BackColor = Color.SteelBlue;
            tabControl1.SelectedIndex = 1;
            global_rowindex_Data = -1;


        }
        public void visible_info_spiski(bool flag)
        {
            label5.Visible = flag;
            label6.Visible = flag;
            label7.Visible = flag;
            label10.Visible = flag;
            label11.Visible = flag;
            label12.Visible = flag;
        }
        private void yt_Button1_Click(object sender, EventArgs e)
        {
            find_profile("friends");
        }
        private void yt_Button2_Click(object sender, EventArgs e)
        {
            find_profile("followers");
        }

        #endregion
        private void paste_Click(object sender, EventArgs e)
        {
            find_id.Text = Clipboard.GetText();
            find_id.Focus();
        }

        private void yt_Button5_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.google.com/") { UseShellExecute = true });
        }
        //работа кнопок приложения в трее
        #region
        private void TSMI_open_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        private void TSMI_close_Click(object sender, EventArgs e)
        {
            close_appendix();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        private void BTN_exit_Click(object sender, EventArgs e)
        {
            close_appendix();
        }

        public void close_appendix()
        {
            connection_DB.Close();
            Application.Exit();
        }


        #endregion

        //анализ пользователя
        #region
        public int[] array_friends;
        public int[] array_followers;
        public int[] array_photos;
        public int[] array_videos;
        public int[] array_audios;
        public int[] array_pades;
        public int[] array_gifts;
        public string[] array_date;

        public string[,] dop_info;

        public int diapozon_from;
        public int diapozon_before;
        public int diapozon;

        public void string_leight(int value)
        {
            array_friends = new int[value];
            array_followers = new int[value];
            array_photos = new int[value];
            array_videos = new int[value];
            array_audios = new int[value];
            array_pades = new int[value];
            array_gifts = new int[value];
            array_date = new string[value];

            dop_info = new string[9, value];
        }

        //достаем из БД информацию
        public void load_profile()
        {

            try
            {
                if ((global_rowindex_Data != -1) & (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT друзья, подписчики, фотографий, видеозаписей, аудиозаписей, сообществ, подарки, дата FROM снимок WHERE id_профиль=" + dataGridView1[5, global_rowindex_Data].Value, connection_DB);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        textBox1.Text = Convert.ToString(table.Rows.Count);
                        //trends_form form = new trends_form();
                        string_leight(table.Rows.Count);

                        global_id_Data = global_id_Data;
                        //form.Text = "тенденции пользователя " + name.Text;

                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            array_friends[i] = Convert.ToInt32(table.Rows[i]["друзья"]);
                            array_followers[i] = Convert.ToInt32(table.Rows[i]["подписчики"]);
                            array_photos[i] = Convert.ToInt32(table.Rows[i]["фотографий"]);
                            array_videos[i] = Convert.ToInt32(table.Rows[i]["видеозаписей"]);
                            array_audios[i] = Convert.ToInt32(table.Rows[i]["аудиозаписей"]);
                            array_pades[i] = Convert.ToInt32(table.Rows[i]["сообществ"]);
                            array_gifts[i] = Convert.ToInt32(table.Rows[i]["подарки"]);
                            array_date[i] = Convert.ToString(table.Rows[i]["дата"]);
                        }

                        adapter = new OleDbDataAdapter("SELECT дата, статус, день_рождения, город, место_учебы, сайт, семейное_положение, номер_телефона, nickname FROM снимок WHERE id_профиль=" + dataGridView1[5, global_rowindex_Data].Value, connection_DB);
                        table = new DataTable();
                        adapter.Fill(table);

                        string[] array = { "дата", "статус", "день_рождения", "город", "место_учебы", "сайт", "семейное_положение", "номер_телефона", "nickname" };

                        for (int i = 0; i < table.Rows.Count; i++)
                            for (int j = 0; j < array.Length; j++)
                                dop_info[j, i] = Convert.ToString(table.Rows[i][array[j]]);

                        diapozon = array_date.Length;

                        for (int i = 0; i < diapozon; i++) // заполняем CB
                            CB_from.Items.Add(array_date[i]);

                        for (int i = 0; i < diapozon; i++)// заполняем CB "до" от перного до последнего
                            CB_before.Items.Add(array_date[i]);

                        if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
                            update_type_data();
                    }
                    else
                        MessageBox.Show("графики можно анализировать только с сохраненными пользователями, имеющими 2 и более снимка", "уведомление");
                }
                else
                    MessageBox.Show("сохраните и выберите профиль", "уведомление");
            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }
        }

        public void update_chart()
        {
            dataGridView4.Visible = false;
            chart1.Visible = true;

            if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
            {
                for (int i = 0; i <= 6; i++)
                    chart1.Series[i].Points.Clear();

                int min_value = 9999;

                for (int i = diapozon_from; i <= diapozon_before; i++)
                {
                    if (Switсh_friends.Checked)
                    {
                        chart1.Series[0].Points.AddXY(array_date[i], array_friends[i]);
                        if (min_value > array_friends[i]) min_value = array_friends[i];
                    }
                    if (Switсh_followers.Checked)
                    {
                        chart1.Series[1].Points.AddXY(array_date[i], array_followers[i]);
                        if (min_value > array_followers[i]) min_value = array_followers[i];
                    }
                    if (Switсh_photos.Checked)
                    {
                        chart1.Series[2].Points.AddXY(array_date[i], array_photos[i]);
                        if (min_value > array_photos[i]) min_value = array_photos[i];
                    }
                    if (Switсh_videos.Checked)
                    {
                        chart1.Series[3].Points.AddXY(array_date[i], array_videos[i]);
                        if (min_value > array_videos[i]) min_value = array_videos[i];
                    }
                    if (Switсh_audios.Checked)
                    {
                        chart1.Series[4].Points.AddXY(array_date[i], array_audios[i]);
                        if (min_value > array_audios[i]) min_value = array_audios[i];
                    }
                    if (Switсh_pades.Checked)
                    {
                        chart1.Series[5].Points.AddXY(array_date[i], array_pades[i]);
                        if (min_value > array_pades[i]) min_value = array_pades[i];
                    }
                    if (Switсh_gifts.Checked)
                    {
                        chart1.Series[6].Points.AddXY(array_date[i], array_gifts[i]);
                        if (min_value > array_gifts[i]) min_value = array_gifts[i];
                    }
                }

                resize_chart(min_value);
            }
        }
        public void update_dgv_dop_info()
        {
            dataGridView4.Columns[0].Width = 130;
            dataGridView4.Columns[0].HeaderText = "дата снимка";
            dataGridView4.Columns[1].HeaderText = "друзья";
            dataGridView4.Columns[2].HeaderText = "подписчики";
            dataGridView4.Columns[3].HeaderText = "фотографии";
            dataGridView4.Columns[4].HeaderText = "видеозаписи";
            dataGridView4.Columns[5].HeaderText = "аудиозаписи";
            dataGridView4.Columns[6].HeaderText = "сообщества";
            dataGridView4.Columns[7].HeaderText = "подарки";

            dataGridView4.Columns[2].ToolTipText = null;
            dataGridView4.Columns[6].ToolTipText = null;
            dataGridView4.Columns[7].ToolTipText = null;

            dataGridView4.Columns[8].Visible = false;

            dataGridView4.Visible = true;
            chart1.Visible = false;

            if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
            {
                //8 столбиков
                int c = 0;

                dataGridView4.RowCount = 0;
                for (int i = diapozon_before - diapozon_from; i >= 0; i--)
                {
                    dataGridView4.RowCount++;
                    dataGridView4[0, c].Value = Convert.ToString(array_date[i]);
                    dataGridView4[1, c].Value = Convert.ToString(array_friends[i]);
                    dataGridView4[2, c].Value = Convert.ToString(array_followers[i]);
                    dataGridView4[3, c].Value = Convert.ToString(array_photos[i]);
                    dataGridView4[4, c].Value = Convert.ToString(array_videos[i]);
                    dataGridView4[5, c].Value = Convert.ToString(array_audios[i]);
                    dataGridView4[6, c].Value = Convert.ToString(array_pades[i]);
                    dataGridView4[7, c].Value = Convert.ToString(array_gifts[i]);

                    c++;
                }
            }
        }
        public void update_dgv_osn_info()
        {
            dataGridView4.Columns[0].Width = 130;
            dataGridView4.Columns[0].HeaderText = "дата снимка";
            dataGridView4.Columns[1].HeaderText = "статус";
            dataGridView4.Columns[2].HeaderText = "ДР";
            dataGridView4.Columns[2].ToolTipText = "день рождения";
            dataGridView4.Columns[3].HeaderText = "город";
            dataGridView4.Columns[4].HeaderText = "место учебы";
            dataGridView4.Columns[5].HeaderText = "сайт";
            dataGridView4.Columns[6].HeaderText = "СП";
            dataGridView4.Columns[6].ToolTipText = "семейное положение";
            dataGridView4.Columns[7].HeaderText = "НТ";
            dataGridView4.Columns[7].ToolTipText = "номер телефона";
            dataGridView4.Columns[8].Visible = true;

            dataGridView4.Visible = true;
            chart1.Visible = false;

            if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
            {

                int c = diapozon_before;

                dataGridView4.RowCount = 0;
                for (int i = 0; i <= diapozon_before - diapozon_from; i++)
                {
                    dataGridView4.RowCount++;
                    for (int j = 0; j < dop_info.GetLength(0); j++)
                        dataGridView4[j, i].Value = Convert.ToString(dop_info[j, c]);
                    c--;
                }
            }
        }

        public void resize_chart(int value)
        {

            int temp = value - (value / 100) * 10;
            if (temp > 90)
                chart1.ChartAreas[0].AxisY.Minimum = temp;
            else
                chart1.ChartAreas[0].AxisY.Minimum = 0;
        }
        public void update_switch()
        {
            RB_chart.Checked = true;
            RB_osn_table.Checked = false;
            RB_dop_table.Checked = false;
            update_chart();
        }


        private void CB_from_SelectionChangeCommitted(object sender, EventArgs e)
        {
            diapozon_from = CB_from.SelectedIndex;
            change_checked();
        }

        private void CB_before_SelectionChangeCommitted(object sender, EventArgs e)
        {
            diapozon_before = CB_before.SelectedIndex;
            change_checked();
        }

        private void change_checked()
        {
            if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
            {

                if (CB_from.SelectedIndex == -1 | CB_before.SelectedIndex == -1)
                {
                    CB_from.SelectedIndex = diapozon_from = 0; // выделить первый элемент
                    CB_before.SelectedIndex = diapozon_before = diapozon - 1; // выделить последний элемент
                }

                if (RB_dop_table.Checked)
                    update_dgv_dop_info();
                if (RB_osn_table.Checked)
                    update_dgv_osn_info();
                if (RB_chart.Checked)
                    update_chart();
            }
        }


        //switch checked changed || radiobutton checked changed 
        #region
        private void Switсh_friends_CheckedChanged(object sender)
        {
            update_switch();
        }

        private void Switсh_followers_CheckedChanged(object sender)
        {
            update_switch();
        }

        private void Switсh_photos_CheckedChanged(object sender)
        {
            update_switch();
        }

        private void Switсh_videos_CheckedChanged(object sender)
        {
            update_switch();
        }

        private void Switсh_audios_CheckedChanged(object sender)
        {
            update_switch();
        }

        private void Switсh_pades_CheckedChanged_1(object sender)
        {
            update_switch();
        }

        private void Switсh_gifts_CheckedChanged(object sender)
        {
            update_switch();
        }
        private void RB_chart_CheckedChanged(object sender, EventArgs e)
        {
            update_chart();
            chart1.Visible = true;
            dataGridView4.Visible = false;
        }

        private void RB_osn_table_CheckedChanged(object sender, EventArgs e)
        {
            update_dgv_osn_info();
            chart1.Visible = false;
            dataGridView4.Visible = true;

        }

        private void RB_dop_table_CheckedChanged(object sender, EventArgs e)
        {
            update_dgv_dop_info();
            chart1.Visible = false;
            dataGridView4.Visible = true;
        }

        #endregion

        #endregion

        public void visible_icon_analiz(bool flag)
        {
            groupBox10.Visible = flag;
            groupBox11.Visible = flag;
            chart1.Visible = flag;
            dataGridView4.Visible = !flag;
            GB_info_spiski.Visible = !flag;
            GB_otobrajenie.Visible = !flag;
            GB_rejim.Visible = !flag;
            GB_sravnenie.Visible = !flag;
            chart1.Visible = flag;
            yt_Button6.Visible = !flag;
        }

        public void update_type_data()
        {
            if (comboBox4.SelectedIndex == 0)
            {
                visible_icon_analiz(true);

                if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
                {
                    //CB_from.Items.Clear();
                    //CB_before.Items.Clear();

                    change_checked();
                }
            }
            else
            {
                visible_icon_analiz(false);
                if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
                {
                    CB_sravnenie_date1.Items.Clear();
                    CB_sravnenie_date2.Items.Clear();
                    CB_otobrajenie_date.Items.Clear();

                    //egoldsRadioButton3.Checked = true;

                    analiz_people();
                }
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_type_data();
        }

        //анализ людей (друзья/подписчики)
        #region
        public int[] array_snimok;
        public string[,] main_spisik;

        public void analiz_people()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT id_снимок, дата FROM снимок WHERE id_профиль=" + global_id_Data, connection_DB);
            DataTable table = new DataTable();
            adapter.Fill(table);

            array_snimok = new int[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                array_snimok[i] = Convert.ToInt32(table.Rows[i]["id_снимок"]);
                CB_otobrajenie_date.Items.Add(table.Rows[i]["дата"]);
                CB_sravnenie_date1.Items.Add(table.Rows[i]["дата"]);
                CB_sravnenie_date2.Items.Add(table.Rows[i]["дата"]);
            }

            CB_sravnenie_date1.SelectedIndex = 0;
            CB_otobrajenie_date.SelectedIndex = table.Rows.Count - 1;
            update_dgv();
        }

        public void update_dgv()
        {
            if ((global_rowindex_Data != -1) && (Convert.ToInt32(dataGridView1[7, global_rowindex_Data].Value) > 0))
                if (RB_friends.Checked)
                {
                    load_people("список_друзей", CB_otobrajenie_date.SelectedIndex);
                }
                else
            if (RB_followers.Checked)
                {
                    load_people("список_подписчиков", CB_otobrajenie_date.SelectedIndex);
                }

            dataGridView4.ClearSelection();
        }

        public void load_people(string name_catalog, int index_date)
        {

            dataGridView4.ClearSelection();

            dataGridView4.Columns[0].Width = 35;
            dataGridView4.Columns[0].HeaderText = "№";
            dataGridView4.Columns[1].HeaderText = "id";
            dataGridView4.Columns[2].HeaderText = "фамилия";
            dataGridView4.Columns[3].HeaderText = "имя";
            dataGridView4.Columns[4].HeaderText = "пол";
            dataGridView4.Columns[5].HeaderText = "город";
            dataGridView4.Columns[6].HeaderText = "nickname";
            dataGridView4.Columns[7].HeaderText = "nickname";

            dataGridView4.Columns[2].ToolTipText = null;
            dataGridView4.Columns[6].ToolTipText = null;

            dataGridView4.Columns[7].Visible = false;
            dataGridView4.Columns[8].Visible = false;

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT id_user, фамилия, имя, пол, город, nickname, id_профиль FROM " + name_catalog + " WHERE id_профиль=" + global_id_Data + " AND id_снимок= " + array_snimok[index_date], connection_DB);
                DataTable table = new DataTable();
                adapter.Fill(table);

                string[] array = { "", "id_user", "фамилия", "имя", "пол", "город", "nickname" };

                main_spisik = new string[7, table.Rows.Count];

                dataGridView4.RowCount = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataGridView4.RowCount++;
                    for (int j = 1; j < array.Length; j++)
                    {
                        dataGridView4[0, i].Value = main_spisik[0, i] = Convert.ToString(i + 1);
                        dataGridView4[j, i].Value = Convert.ToString(table.Rows[i][array[j]]);
                        main_spisik[j, i] = Convert.ToString(table.Rows[i][array[j]]);
                    }
                }

                int value_man = 0; int value_girl = 0;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (main_spisik[4, i] == "мужской") value_man++;
                    if (main_spisik[4, i] == "женский") value_girl++;
                }

                label25.Text = Convert.ToString(dataGridView4.RowCount);
                label27.Text = Convert.ToString(value_man);
                label26.Text = Convert.ToString(value_girl);

            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }

        }


        private void CB_otobrajenie_date_SelectionChangeCommitted(object sender, EventArgs e)
        {
            update_dgv();
        }

        private void RB_friends_CheckedChanged(object sender, EventArgs e)
        {
            update_dgv();
        }

        private void RB_followers_CheckedChanged(object sender, EventArgs e)
        {
            update_dgv();
        }

        private void egoldsRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            GB_otobrajenie.Enabled = true;

            GB_sravnenie.Enabled = false;
            BTN_sravnenie.BackColor = Color.SteelBlue;

            update_dgv();
        }

        private void egoldsRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            GB_otobrajenie.Enabled = false;

            GB_sravnenie.Enabled = true;
            BTN_sravnenie.BackColor = Color.DeepSkyBlue;

            update_dgv();
        }

        private void BTN_sravnenie_Click(object sender, EventArgs e)
        {
            sravnenie();
        }

        public void sravnenie()
        {
            // try
            {
                int index_date1 = CB_sravnenie_date1.SelectedIndex;
                int index_date2 = CB_sravnenie_date2.SelectedIndex;

                if (index_date1 > -1 && index_date2 > -1)
                {
                    string name_catalog = (RB_friends_sravnenie.Checked) ? "список_друзей" : "список_подписчиков";

                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT id_user, фамилия, имя, пол, город, nickname, id_профиль FROM " + name_catalog + " WHERE id_профиль=" + global_id_Data + " AND id_снимок= " + array_snimok[index_date1], connection_DB);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    string[] array = { "", "id_user", "фамилия", "имя", "пол", "город", "nickname" };
                    string[,] temp_array1 = new string[array.Length, table.Rows.Count]; ;

                    for (int i = 0; i < table.Rows.Count; i++)
                        for (int j = 0; j < array.Length; j++)
                            temp_array1[j, i] = Convert.ToString(table.Rows[i][j]);

                    adapter = new OleDbDataAdapter("SELECT id_user, фамилия, имя, пол, город, nickname, id_профиль FROM " + name_catalog + " WHERE id_профиль=" + global_id_Data + " AND id_снимок= " + array_snimok[index_date2], connection_DB);
                    table = new DataTable();
                    adapter.Fill(table);

                    string[,] temp_array2 = new string[array.Length, table.Rows.Count]; ;
                    for (int i = 0; i < table.Rows.Count; i++)
                        for (int j = 0; j < array.Length; j++)
                            temp_array2[j, i] = Convert.ToString(table.Rows[i][j]);

                    if (temp_array1.GetLength(1) > temp_array2.GetLength(1))
                    {
                        main_spisik = temp_array1;
                        temp_array1 = temp_array2;
                    }
                    else
                    {
                        main_spisik = temp_array2;
                    }

                    int lenght = main_spisik.GetLength(1);
                    int n = 0;
                    dataGridView4.RowCount = 0;

                    for (int i = 0; i < lenght; i++)
                    {
                        dataGridView4.RowCount++;
                        for (int c = 0; c < 6; c++)
                        {
                            dataGridView4[0, n].Value = Convert.ToString(n + 1);
                            dataGridView4[c + 1, n].Value = Convert.ToString(main_spisik[c, n]);
                        }
                        n++;
                    }

                    for (int i = lenght - 1; i >= 0; i--)
                    {
                        for (int j = temp_array1.GetLength(1) - 1; j >= 0; j--)
                            if (Convert.ToString(temp_array1[0, j]) == Convert.ToString(dataGridView4[1, i].Value))
                            {
                                dataGridView4.Rows.RemoveAt(i);
                                break;
                            }
                    }

                    int value_man = 0; int value_girl = 0;

                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        if (Convert.ToString(dataGridView4[4, i].Value) == "мужской") value_man++;
                        if (Convert.ToString(dataGridView4[4, i].Value) == "женский") value_girl++;
                    }

                    label25.Text = Convert.ToString(dataGridView4.RowCount);
                    label27.Text = Convert.ToString(value_man);
                    label26.Text = Convert.ToString(value_girl);
                    //MessageBox.Show("различия между списоками успешно выведены", "успех");
                }
                else
                    MessageBox.Show("не выбран диапазон", "ошибка");
            }
            //catch (Exception E) { }
        }
        #endregion

        private void dataGridView4_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView4.Rows[e.RowIndex].Selected = true;
                yt_Button6.BackColor = Color.DeepSkyBlue;
                yt_Button6.Enabled = true;
                dataGridView4.Rows[e.RowIndex].Selected = true;
                dataGridView4.RowsDefaultCellStyle.SelectionForeColor = Color.White;

                global_rowindex_Data = e.RowIndex;
            }
            catch (Exception E) { }
        }

        private void yt_Button6_Click(object sender, EventArgs e)
        {
            find_profile("ather");
        }
        private void domain_TextChanged(object sender, EventArgs e)
        {
            if (domain.Text.Trim() == "")
            {
                yt_Button5.BackColor = Color.SteelBlue;
                yt_Button5.Enabled = false;
                button8.BackColor = Color.SteelBlue;
                button8.Enabled = false;
            }
            else
            {
                yt_Button5.BackColor = Color.DeepSkyBlue;
                yt_Button5.Enabled = true;
                button8.BackColor = Color.DeepSkyBlue;
                button8.Enabled = true;
            }
        }

        private void id_TextChanged(object sender, EventArgs e)
        {
            if (id.Text.Trim() == "")
            {
                button9.BackColor = Color.SteelBlue;
                button9.Enabled = false;
            }
            else
            {
                button9.BackColor = Color.DeepSkyBlue;
                button9.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(Convert.ToString(domain.Text)); } catch (Exception) { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(Convert.ToString(id_user)); } catch (Exception) { }
        }

    }
}
