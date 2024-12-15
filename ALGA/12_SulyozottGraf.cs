
namespace OE.ALGA.Adatszerkezetek
{
    public class SulyozottEgeszGrafEl : EgeszGrafEl, SulyozottGrafEl<int>, IComparable
    {
        //           *   *  ****  ***   ***   *   *   
        //           ** **  *     *  *  *  *   * *     
        //           * * *  ***   ***   ***     *    
        //           *   *  *     * *   * *     *        
        //           *   *  ****  *  *  *  *    *     
        //                                       
        //                                                              
        //  ***   *  *  ***   *  ****  *****  *   *   **   ****
        // *   *  *  *  *  *  *  *       *    ** **  *  *  *   
        // *      ****  ***   *  ****    *    * * *  ****  ****         
        // *   *  *  *  * *   *     *    *    *   *  *  *     *
        //  ***   *  *  *  *  *  ****    *    *   *  *  *  ****
        public int CompareTo(object? other)
        {
            if (other == null) return 1;

            var masikEL = other as SulyozottEgeszGrafEl;
            if (masikEL == null) return 1; 

            int weightComp = this.Suly.CompareTo(masikEL.Suly);

            if (weightComp != 0)
            {
                return weightComp;
            }

            int honnanOsz = this.Honnan.CompareTo(masikEL.Honnan);
            if (honnanOsz != 0)
            {
                return honnanOsz;
            }
            return this.Hova.CompareTo(masikEL.Hova);
        }
        
        public SulyozottEgeszGrafEl(int honnan, int hova, float suly) : base(honnan, hova)
        {
            Suly = suly;
        }

        public float Suly { get; }

        
    }

    public class CsucsmatrixSulyozottEgeszGraf : SulyozottGraf<int, SulyozottEgeszGrafEl>
    {
        int n; float[,] M;
        public int CsucsokSzama { get => n; }
        public CsucsmatrixSulyozottEgeszGraf(int n)
        {
            this.n = n;
            M = new float[n, n];
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++)
                { M[i, j] = float.NaN; }
            }
        }
        

        public int ElekSzama
        {
            get
            {
                int szamlalo = 0;
                for(int i = 0; i < M.GetLength(0); i++)
                {
                    for(int j = 0;j < M.GetLength(1); j++)
                    {
                        if (!float.IsNaN(M[i,j]))
                        { szamlalo++; }
                    }
                }
                return szamlalo;
            }
        }
        

        public Halmaz<SulyozottEgeszGrafEl>       Elek{
            get
            {
                Halmaz<SulyozottEgeszGrafEl> halmaz = new FaHalmaz<SulyozottEgeszGrafEl>();

                for(int i = 0; i< M.GetLength(0); i++)
                {
                    for(int j = 0; j < M.GetLength(1); j++)
                    {
                        if (!float.IsNaN(M[i,j]))
                        { SulyozottEgeszGrafEl puff = new SulyozottEgeszGrafEl(i, j, M[i, j]);
                                        halmaz.Beszur(puff); }
                    }
                }
                return halmaz;
            }
        }
        
        public Halmaz<int> Csucsok {
            get
            {
                Halmaz<int> halmaz = new FaHalmaz<int>();
                for (int i = 0; i   < n; i++) { halmaz.Beszur(i); }
                return halmaz;
            }
        }
        public float Suly(int honnan, int hova)
        {
            if (float.IsNaN(M[honnan, hova]))
            { throw new             NincsElKivetel(); }
            return M[honnan, hova];
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            Halmaz<int> neighbours = new FaHalmaz<int>();
            for(int i = 0;i< n; i++)
            {
                if (!float.IsNaN(M[csucs, i]))
                           neighbours.Beszur(i);
            }
            return neighbours;
        }
        public bool VezetEl(int honnan, int hova)
        {
            return    !float.IsNaN(M[honnan,hova]);
        }

        public void       UjEl(int honnan, int hova, float suly)
        {
            M[honnan, hova]             = suly;
        }

        
    }
    
    public class Utkereses
    {
        
        //⠀⠀⠀⠀⠀⠀⠀⣠⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⣼⡟⠉⠉⠀⠀⠀⠀⢀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⢿⣇⠀⠀⠀⠀⣠⣶⣿⠿⣿⣿⡿⣷⡀⠸⣿⣶⡀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⠘⢿⣆⠀⣠⣾⣿⣿⣿⣶⣿⣿⣶⣿⠁⠀⣠⣿⡇⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢛⣁⣤⣴⣿⠟⠁⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠋⠁⠀⠀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⠀⣿⣿⡟⠉⠉⠀⠀⠈⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⢸⣿⣿⠁⠀⠀⠀⠀⠀⢻⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⣾⣿⠇⠀⠀⠀⠀⠀⠀⠀⢿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⠹⢿⠁⡀⠀⠀⠀⠀⠀⠀⠸⣿⣶⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠀⠀⠀⠀⠀⠀⠀⠀
        public static Szotar<V,float> Dijkstra<V,E>(SulyozottGraf<V,E> g, V start)
        {
            Szotar<V, V> PPP = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            Szotar<V, float> LLL = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            
            PrioritasosSor<V> SSS = new KupacPrioritasosSor<V>(g.CsucsokSzama, (v1,v2) => LLL.Kiolvas(v1) < LLL.Kiolvas(v2)); 

            g.Csucsok.Bejar   (cs =>
            {
                LLL.Beir(cs, float.MaxValue);
                SSS.Sorba(cs);
            });

            LLL.Beir     (start,0);
            SSS.Frissit  (start);

            while (!SSS.Ures)
            {
                V u = SSS.Sorbol();
                g.Szomszedai(u).Bejar              (cs =>
                {
                    if (LLL.Kiolvas(u) + g.Suly(u,cs) < LLL.Kiolvas(cs))
                    {
                        LLL.Beir(cs, LLL.Kiolvas(u) + g.Suly(u, cs));
                    }
                });
                
            }

            return              LLL;
        }
    }

    public class FeszitofaKereses
    {
        //   _,---._
        //   (______ `.  ,-- Christmas Mushroom
        //    ( . . )-'*
        //     \(_)/ 
        //      ) (
        //      """

        public static Halmaz<E> Kruskal<V, E>(SulyozottGraf<V, E> g) where E : SulyozottGrafEl<V>, IComparable where V : IComparable
        {
            Szotar<V, int> vhalmaz = new HasitoSzotarTulcsordulasiTerulettel<V, int>(g.CsucsokSzama);
            PrioritasosSor<E> SSS = new KupacPrioritasosSor<E>(g.ElekSzama, (e1, e2) => e1.CompareTo(e2) == -1);
            Halmaz<E> AAA = new FaHalmaz<E>();
            
            

            int i = 0;

            g.Csucsok.Bejar(cs => { vhalmaz.Beir(cs,i++); });
            g.Elek.Bejar(e => SSS.Sorba(e));
            
            while (!SSS.Ures)
            {
                ;
                E e = SSS.Sorbol();
                i = 0;

                int eFromTemp = vhalmaz.Kiolvas(e.Honnan);
                int eToTemp = vhalmaz.Kiolvas(e.Hova);
                ;
                if (eFromTemp != eToTemp)
                {
                    g.Csucsok.Bejar(cs =>
                    {
                        int csTemp = vhalmaz.Kiolvas(cs);
                        ;
                        if(csTemp == eToTemp)
                        {
                            ;
                            vhalmaz.Beir(cs, eFromTemp);
                        }
                    });
                    AAA.Beszur(e);
                }
                ;
            }


            return AAA;
        }
        
        //  |\/\/\/|  
        //  |      |  
        //  |      |  
        //  | (o)(o)  
        //  C      _) 
        //  | ,___|  
        //  |   /    
        //  /____\    
        // /      \
        public static Szotar<V,V> Prim<V,E>(SulyozottGraf<V,E> g, V start) where V : IComparable
        {
            Szotar<V,V> PPP = new HasitoSzotarTulcsordulasiTerulettel<V,V>(g.CsucsokSzama);
            Halmaz<V> WWW = new FaHalmaz<V>();
            Szotar<V,float> KKK = new HasitoSzotarTulcsordulasiTerulettel<V,float>(g.CsucsokSzama);
            
            V c = start;
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (g1, g2) =>
            {
                if (g1.CompareTo(c) == 0)
                    return true;
                if (g2.CompareTo(c) == 0)
                    return false;

                if (g.VezetEl(c, g1))
                    if (g.VezetEl(c, g2))
                        return g.Suly(c, g1) <= g.Suly(c, g2);
                    else
                        return true;
                else
                if (g.VezetEl(c, g2))
                    return false;
                return true;
            });

            g.Csucsok.Bejar(cs =>
            {
                KKK.Beir(cs, float.MaxValue);
                S.Sorba(cs);
                WWW.Beszur(cs);
            });

            S.Frissit(start);
            KKK.Beir(start,0);
            PPP.Beir(start, start);
            while (!S.Ures)
            {
                c = S.Sorbol();
                WWW.Torol(c);

                g.Szomszedai(c).Bejar(cs =>
                {
                    if(WWW.Eleme(cs) && g.Suly(c,cs) < KKK.Kiolvas(cs)){
                        KKK.Beir(cs, g.Suly(c, cs));
                        PPP.Beir(cs, c);
                    }
                });
            }

            return PPP;
        }
    }
}
