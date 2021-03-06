﻿using System;
using System.Collections.Generic;
//using System.Numerics;
using System.Collections;
using UnityEngine;
using System.IO;



    public class Rect : utility
    {
        public Rect(float x1, float y1, float x2, float y2)
        {
            this.bottomleft.x = x1;
            this.bottomleft.y = y1;
            this.topright.x = x2;
            this.topright.y = y2;
        }

        public Rect clone()
        {
            return new Rect(this.bottomleft.x, this.bottomleft.y, this.topright.x, this.topright.y);
        }

        public Vector2 center()
        {
            return 0.5f * (this.bottomleft + this.topright);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", this.bottomleft, this.topright);
        }

        public Vector2 bottomleft;
        public Vector2 topright;
    }
    public class Program
    {
        public static double distance(Rect a, Rect b)
        {
            // calcul la distance entre deux rectangles

            // a a b b -
            // a b a b
            // a b b a
            // b a a b
            // b a b a
            // b b a a -
            float dx = 0.0f;

            if (a.topright.x < b.bottomleft.x)
            {
                dx = b.bottomleft.x - a.topright.x;
            }
            if (b.topright.x < a.bottomleft.x)
            {
                dx = a.bottomleft.x - b.topright.x;
            }

            float dy = 0.0f;
            if (a.topright.y < b.bottomleft.y)
            {
                dy = b.bottomleft.y - a.topright.y;
            }
            if (b.topright.y < a.bottomleft.y)
            {
                dy = a.bottomleft.y - b.topright.y;
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static double interaction_energy(Rect ra, Rect rb, double eps)
        {
            // l'énergie d'interaction entre deux rectangle
            var s = 0.0;
            var d1 = distance(ra, rb); // distance entre les cadres
            var d2 = (double)(ra.center() - rb.center()).magnitude; // distance entre les centres
            
            // même formule que le potentiel répulsif entre deux changes de même signe
            s += 1.0 / (d1 + eps);
            s += 1.0 / (d2 + eps);
            return s;
        }

        public static double wall_energy(Rect r, Rect wall, double eps)
        {
            // énergie d'interaction avec les murs
            double s = 0.0;
            s += 1.0f / (Math.Max(0.0, wall.topright.x - r.topright.x) + eps);
            s += 1.0f / (Math.Max(0.0, r.bottomleft.x - wall.bottomleft.x) + eps);
            s += 1.0f / (Math.Max(0.0, wall.topright.y - r.topright.y) + eps);
            s += 1.0f / (Math.Max(0.0, r.bottomleft.y - wall.bottomleft.y) + eps);
            return s;
        }

        public static double get_eps(Rect wall)
        {
            return 1e-5f * Math.Min(wall.topright.x - wall.bottomleft.x, wall.topright.y - wall.bottomleft.y);
        }

        public static double total_energy(List<Rect> rs, Rect wall)
        {
            double eps = get_eps(wall);
            double s = 0.0;
            for (int i = 0; i < rs.Count; ++i)
            {
                s += wall_energy(rs[i], wall, eps);

                // pour ne pas calculer deux fois car U(a,b) = U(b,a)
                for (int j = i + 1; j < rs.Count; ++j)
                {
                    s += 2.0 * interaction_energy(rs[i], rs[j], eps);
                }
            }

            return s;
        }

        public static double self_energy(Rect r, List<Rect> rs, Rect wall, int ignore)
        {
            // énergie d'un rectangle avec le reste
            double eps = get_eps(wall);
            double s = 0.0f;
            s += wall_energy(r, wall, eps);
            for (int i = 0; i < rs.Count; ++i)
            {
                if (i == ignore) { continue; }
                s += interaction_energy(r, rs[i], eps);
            }
            return s;
        }

        public static List<Rect> minimize_energy(List<Rect> rs, Rect wall, int steps)
        {
            // calcule les énergies de chaque rectangle avec tous les autres
            var es = new List<double>();
            for (int i = 0; i < rs.Count; ++i)
            {
                es.Add(self_energy(rs[i], rs, wall, i));
            }

            float delta = 1e-2f * Math.Min(wall.topright.x - wall.bottomleft.x, wall.topright.y - wall.bottomleft.y);
            var rand = new System.Random();

            for (int j = 0; j < steps; ++j)
            {
                for (int i = 0; i < rs.Count; ++i)
                {
                    // bouge le rectange aléatoirement
                    Rect r = rs[i].clone();
                    Vector2 d = new Vector2(delta * ((float)rand.NextDouble() - 0.5f), delta * ((float)rand.NextDouble() - 0.5f));
                    r.bottomleft += d;
                    r.topright += d;

                    // interdiction de traverser les murs
                    if (r.topright.x > wall.topright.x || r.topright.y > wall.topright.y || r.bottomleft.x < wall.bottomleft.x || r.bottomleft.y < wall.bottomleft.y)
                    {
                        continue;
                    }
                    var e = self_energy(r, rs, wall, i);
                    if (e < es[i])
                    {
                        // si la nouvelle energie est plus basse on la garde
                        rs[i] = r;
                        es[i] = e;
                    }
                }
            }

            return rs;
        }

        public void PlacePaint(List<Painting> paintings, Vector3 offset, Vector3 direction)
        {
            
            List<Rect> rs = new List<Rect>();
            //Rect item = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            for (int i = 0; i < paintings.Count; i++)
            {
            Rect rect=new Rect(0,0,0,0);

            rect.bottomleft.x = -paintings[i].info.width / 2;
            rect.bottomleft.y = -paintings[i].info.height / 2;
            rect.topright.x = paintings[i].info.width / 2;
            rect.topright.y = paintings[i].info.height / 2;

            rs.Add(rect);
            }
            Rect wall = new Rect((-paintings[0].wallLength/2), -paintings[0].wallLength/2,
                paintings[0].wallLength / 2, paintings[0].wallLength / 2);
            
            Console.WriteLine(total_energy(rs, wall));
            minimize_energy(rs, wall, 1000);
           /* Console.WriteLine(total_energy(rs, wall));
            minimize_energy(rs, wall, 1000);
            Console.WriteLine(total_energy(rs, wall));
            minimize_energy(rs, wall, 1000);
            Console.WriteLine(total_energy(rs, wall));
            minimize_energy(rs, wall, 1000);
            Console.WriteLine(total_energy(rs, wall));
            minimize_energy(rs, wall, 1000);
            Console.WriteLine(total_energy(rs, wall));
            */
            rs.ForEach((x) =>
            {
                
                Console.WriteLine(x.center());
            });
            for(int c = 0; c < paintings.Count; c++)
        {

            Vector3 v = new Vector3(0, 0, 0);
            v.y = rs[c].center().y;
            v.x = (rs[c].center().x * direction).x;
            if (v.z != 0)
            {
                v.z = rs[c].center().x;
                v.x = offset.x;
            }
            else
            {
                v.z = offset.z;
            }
            paintings[c].place(v);


           // paintings[c].place();
        }
            
        }
    }

