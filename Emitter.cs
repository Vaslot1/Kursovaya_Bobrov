using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya_Bobrov
{
     class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int MousePositionX;
        public int MousePositionY;
        public float GravitationX = 0;
        public float GravitationY = 0; // пусть гравитация будет силой один пиксель за такт, нам хватит
        public List<IImpactPoint> impactPoints = new List<IImpactPoint>(); // тут буду хранится точки притяжения
        public void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.Life -= 1; // уменьшаю здоровье
                                    // если здоровье кончилось
                if (particle.Life < 0)
                {
                    // восстанавливаю здоровье
                    particle.Life = 20 + Particle.rand.Next(100);
                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                    // перемещаю частицу в центр

                    var direction = (double)Particle.rand.Next(360);
                    var speed = 1 + Particle.rand.Next(10);

                    particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
                    particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
                    particle.Radius = 2 + Particle.rand.Next(10);
                }
                else
                {
                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                    }

                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;

                    // а это наш старый код
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                }
            }
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < 500)
                {
                    // а у тут уже наш новый класс используем
                    var particle = new ParticleColorful();
                    // ну и цвета меняем
                    particle.FromColor = Color.Yellow;
                    particle.ToColor = Color.FromArgb(0, Color.Magenta);
                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
        }

        public void Render(Graphics g)
        {
            // ну тут так и быть уж сам впишу...
            // это то же самое что на форме в методе Render
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
            foreach (var point in impactPoints)
            {
                point.Render(g);
            }

        }
    }
}
