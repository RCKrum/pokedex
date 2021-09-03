using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using modelo;
using negocio;
using System.IO;



namespace pokedex_form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            //cargar grilla desde db
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                List<Pokemon>ListaObtenida= negocio.listar();
                dgvPokemon.DataSource = ListaObtenida;
                dgvPokemon.Columns[0].Visible = false;
                dgvPokemon.Columns[4].Visible = false;

                pbxPokemon.Load(ListaObtenida[0].UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Pokemon poke = (Pokemon)dgvPokemon.CurrentRow.DataBoundItem;
                pbxPokemon.Load(poke.UrlImagen);
            }
            catch (FileNotFoundException )
            {

                pbxPokemon.Load("https://socialistmodernism.com/wp-content/uploads/2017/07/placeholder-image.png?w=640");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmPokemon ventanaNuevo = new frmPokemon();
            ventanaNuevo.ShowDialog();
            
        }
    }
}
