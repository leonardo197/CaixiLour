using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace caixilour_db
{
    //Iúri Antunes | Leonardo Santos - Verão 2016
    public partial class fm_menu : Form
    {
        int maxrows;
        int x; //=index rows
        int y; //=index cells
        Boolean next=true; //true=frente; false=traz;

        //adapters exclusivos
        SqlDataAdapter da_estores;
        SqlDataAdapter da_portas;
        SqlDataAdapter da_puxadores;
        SqlDataAdapter da_complementos;
        SqlDataAdapter da_assistencias;
        SqlDataAdapter da_despesas;


        SqlConnection cnn;  //coneção
        string sql_string;  //select ... from

        //data_set exclusivos
        DataSet dat_set_estores;
        DataSet dat_set_puxadores;
        DataSet dat_set_portas;
        DataSet dat_set_complementos;
        DataSet dat_set_assistencias;
        DataSet dat_set_despesas;

        SqlCommandBuilder cmdBldr;

        public fm_menu()
        {
            InitializeComponent();
        }     
        private void cb_produto_SelectedIndexChanged(object sender, EventArgs e)
        {

            string produto = cb_produto.Text;
            switch (produto)
            {
                case "Portas":
                    pb_produtos.BackgroundImage = caixilour_db.Properties.Resources.portas;
                    maxrows = dat_set_portas.Tables[0].Rows.Count;
                    break;
                case "Puxadores":
                    pb_produtos.BackgroundImage = caixilour_db.Properties.Resources.puxadores;
                    break;
                case "Estores":
                    pb_produtos.BackgroundImage = caixilour_db.Properties.Resources.estores;
                    break;
                case "Assintências":
                    //pb_produtos.BackgroundImage = caixilour_db.Properties.Resources.estores;
                    break;
                case "Rede Mosqueteira":
                    //pb_produtos.BackgroundImage = caixilour_db.Properties.Resources.estores;
                    break;
                case "Complementos":
                    //pb_produtos.BackgroundImage = caixilour_db.Properties.Resources.estores;
                    break;
            }
        }
        private void SB_preco_Scroll(object sender, ScrollEventArgs e)
        {
            lb_preco_total.Text = "0€ - " + Convert.ToString(SB_preco.Value) + "€";
        }
        private void fm_menu_Load(object sender, EventArgs e)
        {
            string connetionString = null;
            //caminho:
            connetionString = "Data Source=192.168.3.13,1433; Network Library=DBMSSOCN;Initial Catalog=caixilour_estoque; User ID=admin;Password=caixilour";
            cnn = new SqlConnection(connetionString);
            //cnn.Open();

            ////ligar - estoros
            //sql_string = "select * from estores";
            //da_estores = new SqlDataAdapter(sql_string, cnn);
            //dat_set_estores = new System.Data.DataSet();
            //da_estores.Fill(dat_set_estores, "estores");

            ////ligar - portas
            //sql_string = "select * from portas";
            //da_portas= new SqlDataAdapter(sql_string, cnn);
            //dat_set_portas = new System.Data.DataSet();
            //da_portas.Fill(dat_set_portas, "portas");

            ////ligar - puxadores
            //sql_string = "select * from puxadores";
            //da_puxadores = new SqlDataAdapter(sql_string, cnn);
            //dat_set_puxadores = new System.Data.DataSet();
            //da_puxadores.Fill(dat_set_puxadores, "puxadores");

            ////ligaasdfassdr - assistências
            //sql_string = "select * from assistencias";
            //da_assistencias = new SqlDataAdapter(sql_string, cnn);
            //dat_set_assistencias = new System.Data.DataSet();
            //da_assistencias.Fill(dat_set_assistencias, "assistencias");

            ////ligar - complementos
            //sql_string = "select * from complementos";
            //da_complementos = new SqlDataAdapter(sql_string, cnn);
            //dat_set_complementos = new System.Data.DataSet();
            //da_complementos.Fill(dat_set_complementos, "complementos");

            ////ligar - complementos
            //sql_string = "select * from despesas";
            //da_despesas = new SqlDataAdapter(sql_string, cnn);
            //dat_set_despesas = new System.Data.DataSet();
            //da_despesas.Fill(dat_set_despesas, "despesas");


            //cnn.Close();

            //variaveis | objetos:
            pb_odf_img.Visible = false; //imagem de conversão binária

            //decoraçao
            pb_prod_img1.Location = new Point(63,344); 
            pb_prod_img2.Location = new Point(63, 385);
            pb_prod_img3.Location = new Point(63, 431);
            pb_prod_img4.Location = new Point(63, 467);
            pb_prod_img5.Location = new Point(63, 509);
            pb_prod_img6.Location = new Point(63, 545);
            pb_barra.Location = new Point(36, 599);

            tb_pq_id.Visible = false;
            cb_admin_pesquisas.Visible = false;
            lb_dc_id.Visible = false;
            pb_admin_on1.Visible = false;
            pb_admin_on2.Visible = false;
            pb_admin_on3.Visible = false;
            pb_admin_on4.Visible = false;
            pb_admin_on5.Visible = false;
            pb_admin_on6.Visible = false;
            cb_pq_cor.Visible = false;
            hSB_admin.Visible = false;
            lb_admin_preco.Visible = false;
            tb_admin_preco.Visible = false;
            pb_admin_fundo.Visible = false;
            cb_pq_cextras1.Visible = false;
            cb_pq_cextras2.Visible = false;
            tb_pq_ref.Visible = false;
        }
        private void tm_enables_Tick(object sender, EventArgs e)
        {
            //cor - selecionar grellha
            if (chb_grelha.Checked == true)
            {
                chb_grelha.ForeColor = Color.DarkRed;
                chb_vidro.Checked = true;
            }
            else
            {
                chb_grelha.ForeColor = Color.Black;
            }

            //cor - selecionar vidro
            if (chb_vidro.Checked == true)
            {
                chb_vidro.ForeColor = Color.DarkRed;
            }
            else
            {
                chb_vidro.ForeColor = Color.Black;
            }
        }
        private void chb_grelha_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_grelha.Checked == true)
            {
                MessageBox.Show("Ao habilitar c/grelha também irá selecionar c/vidro. Não existem portas com grelha e sem vidro.", "Porta - Modelo",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
           
        }
        private void cb_Admin_produtos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //carregar informação:
            string produto = cb_Admin_produtos.Text;
            switch (produto)
            {
                case "Portas":
                    pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.portas;
                    maxrows = dat_set_portas.Tables[0].Rows.Count;

                    dat_set_portas = new System.Data.DataSet();
                    sql_string = "select * from portas";
                    da_portas= new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_portas);

                    da_portas.Fill(dat_set_portas,"portas");
                    data_sett.DataSource = (dat_set_portas);
                    data_sett.DataMember = "portas";  
                 break;
                case "Puxadores":
                    pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.puxadores;

                    dat_set_puxadores = new System.Data.DataSet();
                    sql_string = "select * from puxadores";
                    da_puxadores= new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_puxadores);

                    da_puxadores.Fill(dat_set_puxadores,"puxadores");
                    data_sett.DataSource = (dat_set_puxadores);
                    data_sett.DataMember = "puxadores";
                    data_sett.Columns[0].Visible = false;
                    break;
                case "Estores":
                    pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.estores;

                    dat_set_estores = new System.Data.DataSet();
                    sql_string = "select * from estores";
                    da_estores= new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_estores);

                    da_estores.Fill(dat_set_estores, "estores");
                    data_sett.DataSource = (dat_set_estores);
                    data_sett.DataMember = "estores";
                    data_sett.Columns[0].Visible = false;
                    break;
                case "Assistências":
                    pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.assistencias;

                    dat_set_assistencias = new System.Data.DataSet();
                    sql_string = "select * from assistencias";
                    da_assistencias = new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_assistencias);

                    da_assistencias.Fill(dat_set_assistencias, "assistencias");
                    data_sett.DataSource = (dat_set_assistencias);
                    data_sett.DataMember = "assistencias";
                    data_sett.Columns[0].Visible = false;
                    break;
                case "Complementos":
                    pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.complementos;

                    dat_set_complementos = new System.Data.DataSet();
                    sql_string = "select * from complementos";
                    da_complementos = new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_complementos);

                    da_complementos.Fill(dat_set_complementos, "complementos");
                    data_sett.DataSource = (dat_set_complementos);
                    data_sett.DataMember = "complementos";
                    data_sett.Columns[0].Visible = false;
                    break;
                case "Despesas":
                    pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.despesas;

                    dat_set_despesas = new System.Data.DataSet();
                    sql_string = "select * from despesas";
                    da_despesas = new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_despesas);

                    da_despesas.Fill(dat_set_despesas, "despesas");
                    data_sett.DataSource = (dat_set_despesas);
                    data_sett.DataMember = "despesas";
                    data_sett.Columns[0].Visible = false;
                    break;                    
            }           
            data_sett.Columns[0].Visible = false;
        }
        private void bt_guardar_Click(object sender, EventArgs e)
        {
            string produto = cb_Admin_produtos.Text;
            switch (produto)
            {
                case "Portas":
                    //adicionar
                    da_portas.Update(dat_set_portas, "portas");
                    break;
                case "Puxadores":
                    //adicionar
                    da_puxadores.Update(dat_set_puxadores, "puxadores");
                    break;
                case "Estores":
                    //adicionar
                    da_estores.Update(dat_set_estores, "estores");
                    break;
                case "Assintências":
                    //pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.estores;
                    break;
                case "Rede Mosqueteira":
                    //pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.estores;
                    break;
                case "Complementos":
                    //pb_admin_produtos.BackgroundImage = caixilour_db.Properties.Resources.estores;
                    break;
            }
        }
        private void data_sett_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string produto = cb_Admin_produtos.Text;
            switch (produto)
            {
                case "Portas":
                    //adicionar imagem:
                    {
                        x = e.RowIndex;
                        y = e.ColumnIndex;

                        if (y == 9)
                        {
                        if (data_sett.Rows[x].Cells["Imagem"].Value.ToString() == "")
                            {
                            OFD_img.Filter = "seleciona a imagem (*.jpg;*.png)|*.jpg;*.png";
                            if (OFD_img.ShowDialog() == DialogResult.OK)
                            {
                                pb_odf_img.Image = Image.FromFile(OFD_img.FileName);
                                MemoryStream ms = new MemoryStream();
                                pb_odf_img.Image.Save(ms, pb_odf_img.Image.RawFormat);
                                byte[] img = ms.ToArray();
                                data_sett.Rows[x].Cells["Imagem"].Value = img;
                            }
                        }
                        }
                    }
                    break;
                case "Puxadores":
                  
                    break;
                case "Estores":
                   
                    break;
                case "Assintências":
                    //adicionar imagem:
                    break;
                case "Rede Mosqueteira":
                    //adicionar imagem:
                    break;
                case "Complementos":
                    //adicionar imagem:
                    break;
            }
        }
        private void pb_admin_next_Click(object sender, EventArgs e)
        {
            if (next == true)
            {
               //mudança entre filtros:
               lb_filtrar_tabelas.Text = "Pesquisas:";
               //decoraçao
               pb_prod_img1.Location = new Point(45, 580);
               pb_prod_img2.Location = new Point(81, 580);
               pb_prod_img3.Location = new Point(117, 580);
               pb_prod_img4.Location = new Point(153, 580);
               pb_prod_img5.Location = new Point(187, 580);
               pb_prod_img6.Location = new Point(223, 580);
               pb_barra.Location = new Point(36, 622);

               pb_admin_on1.Visible = true;
               pb_admin_on2.Visible = true;
               pb_admin_on3.Visible = true;
               pb_admin_on4.Visible = true;
               pb_admin_on5.Visible = true;
               pb_admin_on6.Visible = true;

             

               cb_admin_pesquisas.Visible = true;

               cb_tb_1.Visible = false;
               if (cb_tb_1.Checked == false)
               {
                  pb_admin_on1.BackColor = Color.Red;
               }
               else
               {
                  pb_admin_on1.BackColor = Color.Green;
               }

               cb_tb_2.Visible = false;
               if (cb_tb_2.Checked == false)
               {
                  pb_admin_on2.BackColor = Color.Red;
               }
               else
               {
                  pb_admin_on2.BackColor = Color.Green;
               }
            
               cb_tb_3.Visible = false;
               if (cb_tb_3.Checked == false)
               {
                  pb_admin_on3.BackColor = Color.Red;
               }
               else
               {
                  pb_admin_on3.BackColor = Color.Green;
               }

               cb_tb_4.Visible = false;
               if (cb_tb_4.Checked == false)
               {
                  pb_admin_on4.BackColor = Color.Red;
               }
               else
               {
                  pb_admin_on4.BackColor = Color.Green;
               }

               cb_tb_5.Visible = false;
               if (cb_tb_5.Checked == false)
               {
                  pb_admin_on5.BackColor = Color.Red;
               }
               else
               {
                  pb_admin_on5.BackColor = Color.Green;
               }

               cb_tb_6.Visible = false;
               if (cb_tb_6.Checked == false)
               {
                  pb_admin_on6.BackColor = Color.Red;
               }
               else
               {
                  pb_admin_on6.BackColor = Color.Green;
               }

               pb_dc_1.Visible = false;
               pb_dc_2.Visible = false;
               pb_dc_3.Visible = false;
               pb_dc_4.Visible = false;
               pb_dc_5.Visible = false;
               pb_dc_6.Visible = false;
               pb_dc_7.Visible = false;
               pb_dc_8.Visible = false;
               pb_dc_9.Visible = false;
               pb_dc_10.Visible = false;
               pb_dc_11.Visible = false;
               pb_dc_12.Visible = false;

               pb_admin_next.BackgroundImage = caixilour_db.Properties.Resources.traz;

             //trocar sentido
               next = false;

            }
            else
            {
                //mudança entre filtros:
                lb_filtrar_tabelas.Text = "Tabelas:";
                //decoraçao
                pb_prod_img1.Location = new Point(63, 344);
                pb_prod_img2.Location = new Point(63, 385);
                pb_prod_img3.Location = new Point(63, 431);
                pb_prod_img4.Location = new Point(63, 467);
                pb_prod_img5.Location = new Point(63, 509);
                pb_prod_img6.Location = new Point(63, 545);
                pb_barra.Location = new Point(36, 599);

                cb_admin_pesquisas.Visible = false;
                tb_pq_id.Visible = false;
                lb_dc_id.Visible = false;
                cb_pq_cextras1.Visible = false;
                cb_pq_cextras2.Visible = false;
                hSB_admin.Visible = false;
                lb_admin_preco.Visible = false;
                tb_admin_preco.Visible = false;
                pb_admin_fundo.Visible = false;
                tb_pq_ref.Visible = false;

                pb_dc_1.Visible = true;
                pb_dc_2.Visible = true;
                pb_dc_3.Visible = true;
                pb_dc_4.Visible = true;
                pb_dc_5.Visible = true;
                pb_dc_6.Visible = true;
                pb_dc_7.Visible = true;
                pb_dc_8.Visible = true;
                pb_dc_9.Visible = true;
                pb_dc_10.Visible = true;
                pb_dc_11.Visible = true;
                pb_dc_12.Visible = true;

                cb_tb_1.Visible = true;
                cb_tb_2.Visible = true;
                cb_tb_3.Visible = true;
                cb_tb_4.Visible = true;
                cb_tb_5.Visible = true;
                cb_tb_6.Visible = true;

                pb_admin_on1.Visible = false;
                pb_admin_on2.Visible = false;
                pb_admin_on3.Visible = false;
                pb_admin_on4.Visible = false;
                pb_admin_on5.Visible = false;
                pb_admin_on6.Visible = false;

                pb_admin_next.BackgroundImage = caixilour_db.Properties.Resources.frente;

                //trocar sentido
                next = true;
            }
        }
        private void cb_admin_pesquisas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pesquisa = cb_admin_pesquisas.Text;
            switch (pesquisa)
            {
                case "ID":
                    lb_dc_id.Visible = true;
                    tb_pq_id.Visible = true;

                    cb_pq_cor.Visible = false;
                    hSB_admin.Visible = false;
                    lb_admin_preco.Visible = false;
                    tb_admin_preco.Visible = false;
                    pb_admin_fundo.Visible = false;
                    cb_pq_cextras1.Visible = false;
                    cb_pq_cextras2.Visible = false;
                    tb_pq_ref.Visible = false;
                    break;
                case "Cores":
                    cb_pq_cor.Visible = true;

                    dat_set_portas = new System.Data.DataSet();
                    sql_string = "select Cor from portas'";
                    da_portas = new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_portas);

                    da_portas.Fill(dat_set_portas, "pes");
                    data_sett.DataSource = (dat_set_portas);
                    data_sett.DataMember = "pes";
                    for (int i = 1; i <= 5; i++)
                    {
                        Console.WriteLine(i);
                    }
                    

                    lb_dc_id.Visible = false;
                    tb_pq_id.Visible = false;
                    hSB_admin.Visible = false;
                    lb_admin_preco.Visible = false;
                    tb_admin_preco.Visible = false;
                    pb_admin_fundo.Visible = false;
                    cb_pq_cextras1.Visible = false;
                    cb_pq_cextras2.Visible = false;
                    tb_pq_ref.Visible = false;
                    break;
                case "Preço":
                    hSB_admin.Visible = true;
                    lb_admin_preco.Visible = true;
                    tb_admin_preco.Visible = true;
                    pb_admin_fundo.Visible = true;

                    cb_pq_cor.Visible = false;
                    lb_dc_id.Visible = false;
                    tb_pq_id.Visible = false;
                    cb_pq_cextras1.Visible = false;
                    cb_pq_cextras2.Visible = false;
                    tb_pq_ref.Visible = false;
                    break;
                case "c/ Extras":
                    hSB_admin.Visible = false;
                    lb_admin_preco.Visible = false;
                    tb_admin_preco.Visible = false;
                    pb_admin_fundo.Visible = false;
                    cb_pq_cor.Visible = false;
                    lb_dc_id.Visible = false;
                    tb_pq_id.Visible = false;
                    tb_pq_ref.Visible = false;

                    cb_pq_cextras1.Visible = true;
                    cb_pq_cextras2.Visible = true;

                    break;
                case "Referência":
                    tb_pq_ref.Visible = true;

                     hSB_admin.Visible = false;
                    lb_admin_preco.Visible = false;
                    tb_admin_preco.Visible = false;
                    pb_admin_fundo.Visible = false;
                    cb_pq_cor.Visible = false;
                    lb_dc_id.Visible = false;
                    tb_pq_id.Visible = false;
                    cb_pq_cextras1.Visible = false;
                    cb_pq_cextras2.Visible = false;
                    break;
            }
        }
        private void hSB_admin_Scroll(object sender, ScrollEventArgs e)
        {
            lb_admin_preco.Text = "0€ - " + Convert.ToString(hSB_admin.Value) + "€";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cb_tb_1.Checked == true)
            {

                //tb_pq_ID
                if (cb_admin_pesquisas.SelectedItem =="ID")
                {   
                    dat_set_portas = new System.Data.DataSet();
                    sql_string = "select * from portas  where ID ='" + (tb_pq_id.Text) + "'";
                    da_portas = new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_portas);

                    da_portas.Fill(dat_set_portas, "pes");
                    data_sett.DataSource = (dat_set_portas);
                    data_sett.DataMember = "pes";
                    data_sett.Columns[0].Visible = false;
                }

                //tb_pq_Cores
                if (cb_admin_pesquisas.SelectedItem == "Cores")
                {
                    dat_set_portas = new System.Data.DataSet();
                    sql_string = "select * from portas  where Cores ='" + (cb_pq_cor.SelectedItem) + "'";
                    da_portas = new SqlDataAdapter(sql_string, cnn);
                    cmdBldr = new SqlCommandBuilder(da_portas);

                    da_portas.Fill(dat_set_portas, "pes");
                    data_sett.DataSource = (dat_set_portas);
                    data_sett.DataMember = "pes";
                    data_sett.Columns[0].Visible = false;
                }
            }

            if (cb_tb_2.Checked == true)
            {
                
            }
  
            if (cb_tb_3.Checked == true)
            {
                
            }
            
            if (cb_tb_4.Checked == true)
            {
                
            }          


            if (cb_tb_5.Checked == true)
            {
 
            }

            if (cb_tb_6.Checked == true)
            {
                
            }
           
        }

         
    }
}