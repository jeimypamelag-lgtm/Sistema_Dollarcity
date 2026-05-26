using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Dollarcity
{
    public partial class Inicio : Form
    {
        int indiceGrande = 0;
        int indicePeque = 0;

        string[] sliderGrande =
        {
            @"SliderGrande\slide1.jpg",
            @"SliderGrande\slide2.jpg",
            @"SliderGrande\slide3.jpg"
        };
        string[] imagenesMama =
        {
            @"SlideMama\mama1.jpg",
            @"SlideMama\mama2.jpg",
            @"SlideMama\mama3.jpg"
        };
        string[] imagenesJardin =
        {
            @"SliderJardin\jardin1.jpg",
            @"SliderJardin\jardin2.jpg",
            @"SliderJardin\jardin3.jpg"
        };


        public Inicio()
        {
            InitializeComponent();
            // Configuraciones visuales 
            pictureSliderGrande.SizeMode = PictureBoxSizeMode.Zoom;
            picSliderMama.SizeMode = PictureBoxSizeMode.Zoom;
            picSliderJardin.SizeMode = PictureBoxSizeMode.Zoom;

            CargarImagenesIniciales();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            timerGrande.Start();
            timerPeque.Start();
        }

        private void timerGrande_Tick(object sender, EventArgs e)
        {
            indiceGrande = (indiceGrande + 1) % sliderGrande.Length;
            ActualizarImagenSlider(pictureSliderGrande, sliderGrande[indiceGrande]);
        }

        private void CargarImagenesIniciales()
        {
            // 1. Cargar Slider Grande Inicial
            string rutaG = Path.Combine(Application.StartupPath, sliderGrande[0]);
            if (File.Exists(rutaG)) pictureSliderGrande.Image = Image.FromFile(rutaG);

            // 2. Cargar Slider Mamá Inicial (Agregamos verificación y mensajito de alerta si falla)
            string rutaM = Path.Combine(Application.StartupPath, imagenesMama[0]);
            if (File.Exists(rutaM))
            {
                picSliderMama.Image = Image.FromFile(rutaM);
            }
            else
            {
                // Si no existe, te mostrará un aviso con la ruta exacta donde debes meter la foto
                MessageBox.Show("No se encontró la imagen de Mamá en: " + rutaM, "Falta Archivo");
            }

            // 3. Cargar Slider Jardín Inicial
            string rutaJ = Path.Combine(Application.StartupPath, imagenesJardin[0]);
            if (File.Exists(rutaJ)) picSliderJardin.Image = Image.FromFile(rutaJ);
        }

        private void timerPeque_Tick(object sender, EventArgs e)
        {
            // Avanzar el índice de los pequeños
            indicePeque = (indicePeque + 1) % 3; // Son 3 imágenes por campaña

            // Actualizar el banner de Mamá
            ActualizarImagenSlider(picSliderMama, imagenesMama[indicePeque]);

            // Actualizar el banner de Jardinería
            ActualizarImagenSlider(picSliderJardin, imagenesJardin[indicePeque]);
        }

        private void ActualizarImagenSlider(PictureBox pb, string rutaRelativa)
        {
            string rutaCompleta = Path.Combine(Application.StartupPath, rutaRelativa);
            if (File.Exists(rutaCompleta))
            {
                if (pb.Image != null) pb.Image.Dispose();
                pb.Image = Image.FromFile(rutaCompleta);
            }
        }


        private void HacerBotonRedondo(Button boton)
        {
            // Creamos un camino de dibujo geométrico
            System.Drawing.Drawing2D.GraphicsPath alfombra = new System.Drawing.Drawing2D.GraphicsPath();

            // Dibujamos un círculo perfecto del tamaño exacto del botón
            alfombra.AddEllipse(0, 0, boton.Width, boton.Height);

            // Le decimos al botón que su región de clic y vista ahora es solo ese círculo
            boton.Region = new Region(alfombra);
        }
    }
}
