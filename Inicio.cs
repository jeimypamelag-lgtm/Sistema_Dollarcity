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
        string rolUsuario; //variabe del usuario

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


        public Inicio(string rol)
        {
            InitializeComponent();

            // Configuraciones visuales 
            pictureSliderGrande.SizeMode = PictureBoxSizeMode.Zoom;
            picSliderMama.SizeMode = PictureBoxSizeMode.Zoom;
            picSliderJardin.SizeMode = PictureBoxSizeMode.Zoom;

            CargarImagenesIniciales();

            //carga los primeros productos
            CargarProductosIniciales();

            rolUsuario = rol;
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
        //----------------------------------------------------------------------------------------
        //metodo para cargar productos iniciales
        private void CargarProductosIniciales()
        {
            if (Datos.ListaProductos.Count > 0)
                return;

            //JARDINERIA

            Datos.ListaProductos.Add(new Producto
            {
                Id = 1,
                Nombre = "Maceta",
                Precio = 5.99m,
                Descripcion = "Maceta decorativa",
                Stock = true,
                Categoria = "Jardineria",
                Imagen = "maceta.jpg"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 2,
                Nombre = "Tijera",
                Precio = 3.50m,
                Descripcion = "Tijera para jardín",
                Stock = true,
                Categoria = "Jardineria",
                Imagen = "tijera.jpg"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 3,
                Nombre = "Bolsa jardinera",
                Precio = 2.25m,
                Descripcion = "Bolsa de basura grande",
                Stock = false,
                Categoria = "Jardineria",
                Imagen = "bolsa.png"
            });

            //COCINA

            Datos.ListaProductos.Add(new Producto
            {
                Id = 4,
                Nombre = "Bandeja",
                Precio = 3.25m,
                Descripcion = "Bandeja de aluminio para hornear",
                Stock = false,
                Categoria = "Cocina",
                Imagen = "bandeja.jpg"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 5,
                Nombre = "Batidor",
                Precio = 1.25m,
                Descripcion = "Batidor de reposteria",
                Stock = false,
                Categoria = "Cocina",
                Imagen = "batidor.jpg"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 6,
                Nombre = "Cuchillo",
                Precio = 2.75m,
                Descripcion = "Cuchillo",
                Stock = false,
                Categoria = "Cocina",
                Imagen = "cuchillo.jpg"
            });

            //HOGAR

            Datos.ListaProductos.Add(new Producto
            {
                Id = 7,
                Nombre = "Jarron",
                Precio = 5m,
                Descripcion = "Florero transparente",
                Stock = false,
                Categoria = "Hogar",
                Imagen = "jarron.png"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 8,
                Nombre = "Asistin",
                Precio = 3.50m,
                Descripcion = "Bote de asistin para trapear",
                Stock = true,
                Categoria = "Hogar",
                Imagen = "asistin.png"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 9,
                Nombre = "Vela",
                Precio = 1.50m,
                Descripcion = "Vela aromátia",
                Stock = false,
                Categoria = "Hogar",
                Imagen = "vela.png"
            });

            //CUDADO PERSONAL

            Datos.ListaProductos.Add(new Producto
            {
                Id = 10,
                Nombre = "Cepillo",
                Precio =1m,
                Descripcion = "Cepillo de dientes",
                Stock = true,
                Categoria = "Cuidado Personal",
                Imagen = "cepillo.png"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 11,
                Nombre = "Toallitas humedas",
                Precio =2.50m,
                Descripcion = "Toallitas humedas",
                Stock = false,
                Categoria = "Cuidado Personal",
                Imagen = "crema.jpg"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 12,
                Nombre = "Toallas nocturnas",
                Precio = 1.50m,
                Descripcion = "Toallas intimas nocturnas saba",
                Stock = true,
                Categoria = "Cuidado Personal",
                Imagen = "saba.jpg"
            });

            //MANUALIDADES

            Datos.ListaProductos.Add(new Producto
            {
                Id = 13,
                Nombre = "Cartulinas",
                Precio = 1m,
                Descripcion = "Cartulinas de colores",
                Stock = false,
                Categoria = "Manualidades",
                Imagen = "cartulina.jpg"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 14,
                Nombre = "Plumones",
                Precio = 4.30m,
                Descripcion = "Plumones de colores",
                Stock = true,
                Categoria = "Manualidades",
                Imagen = "plumones.jpg"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 15,
                Nombre = "Pinceles",
                Precio = 3.40m,
                Descripcion = "Pincels para pintar",
                Stock = false,
                Categoria = "Manualidades",
                Imagen = "pinceles.png"
            });

            //JUGUETES

            Datos.ListaProductos.Add(new Producto
            {
                Id = 16,
                Nombre = "HotWheel",
                Precio = 2m,
                Descripcion = "Carrito de juguete",
                Stock = false,
                Categoria = "Juguetes",
                Imagen = "hotwheel.jpg"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 17,
                Nombre = "Squishy",
                Precio = 1.50m,
                Descripcion = "Juguete desestresante",
                Stock = true,
                Categoria = "Juguetes",
                Imagen = "squishy.png"
            });

            Datos.ListaProductos.Add(new Producto
            {
                Id = 18,
                Nombre = "Rompecabezas",
                Precio = 4m,
                Descripcion = "Set de rompecabezas",
                Stock = true,
                Categoria = "Juguetes",
                Imagen = "rompecabezas.jpg"
            });
        }


        private void btnJardinería_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(
        "Jardineria",
        rolUsuario
    );

            ventana.Show();
        }

        private void btnCocina_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(
        "Cocina",
        rolUsuario
    );

            ventana.Show();
        }

        private void btnHogar_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(
        "Hogar",
        rolUsuario
    );

            ventana.Show();
        }

        private void btnCuidadoPersonal_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(
        "Cuidado Personal",
        rolUsuario
    );

            ventana.Show();
        }

        private void btnManualidades_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(
        "Manualidades",
        rolUsuario
    );

            ventana.Show();
        }

        private void btnJuguetes_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(
        "Juguetes",
        rolUsuario
    );

            ventana.Show();
        }
    }
}
