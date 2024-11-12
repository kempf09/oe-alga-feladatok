namespace OE.ALGA.Adatszerkezetek
{



    public class EgeszGrafEl:GrafEl<int> , IComparable
    {
        
        public int Honnan { get; }
        public int Hova { get; }
        
        public EgeszGrafEl(int honnan, int hova)
        {
            Honnan = honnan;
            Hova = hova;
        }


        public virtual int CompareTo(object? obj)
        {
            if (obj != null && obj is EgeszGrafEl b)
            {
                if (Honnan!=b.Honnan)
                {
                    return Honnan.CompareTo(b.Honnan);
                }
                else return Hova.CompareTo(b.Hova);
                ;
            }
            else throw new InvalidOperationException();
        }

        
        
    }

    public class CsucsmatrixSulyozatlanEgeszGraf : SulyozatlanGraf<int, EgeszGrafEl>
    {
        int n = 0;
        bool[,] M;

        public CsucsmatrixSulyozatlanEgeszGraf(int n)
        {
            this.n = n;
            M = new bool[n, n];
        }
        //######################################################################
        public int CsucsokSzama => n;
        //######################################################################
        public int ElekSzama
        {
            get
            {
                int elekSzama = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j]) elekSzama++;
                    }
                }
                return elekSzama;
            }
        }
        //######################################################################
        public Halmaz<int> Csucsok
        {
            get
            {
                Halmaz<int> csucsok = new FaHalmaz<int>();
                for (int csucs = 0; csucs < n; csucs++)
                {
                    csucsok.Beszur(csucs);
                }
                return csucsok;
            }
        }
        //######################################################################
        public Halmaz<EgeszGrafEl> Elek
        {
            get
            {
                Halmaz<EgeszGrafEl> elek = new FaHalmaz<EgeszGrafEl>();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j])
                        {
                            elek.Beszur(new EgeszGrafEl(i, j));
                        }
                    }
                }
                return elek;
            }
        }
        //######################################################################
        public bool VezetEl(int honnan, int hova)
        {
            return M[honnan, hova];
        }
        //######################################################################
        public Halmaz<int> Szomszedai(int csucs)
        {
            Halmaz<int> szomszedok = new FaHalmaz<int>();
            for (int i = 0; i < n; i++)
            {
                if (M[csucs, i])
                {
                    szomszedok.Beszur(i);
                }
            }
            return szomszedok;
        }

        
        //######################################################################
        public void UjEl(int honnan, int hova)
        {
            M[honnan, hova] = true;
        }
    }

    public static class GrafBejarasok
    {
        public static Halmaz<V> SzelessegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet)
            where V : IComparable
            where E : GrafEl<V>
        {
            Sor<V> S = new LancoltSor<V>();
            Halmaz<V> F = new FaHalmaz<V>();
            
            S.Sorba(start);
            F.Beszur(start);

            while (!S.Ures)
            {
                V k = S.Sorbol();
                muvelet(k);
                
                Halmaz<V> szomsz= g.Szomszedai(k);
                List<V> szomszLista = new List<V>();
                
                szomsz.Bejar(x=>szomszLista.Add(x));

                for (int i = 0; i < szomszLista.Count; i++)
                {
                    V x = szomszLista[i];

                    if (!F.Eleme(x))
                    {
                        S.Sorba(x);
                        F.Beszur(x);
                    }
                }
            }
            
            return F;
        }

        public static Halmaz<V> MelysegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet)
            where V : IComparable
            where E : GrafEl<V>
        {
            
            Halmaz<V> F = new FaHalmaz<V>();
            MelysegiBejarasRekurzio(g, start, F, muvelet);
            return F;
        }

        
        public static void MelysegiBejarasRekurzio<V, E>(Graf<V, E> g, V k, Halmaz<V> F, Action<V> muvelet)
            where V : IComparable
            where E : GrafEl<V>
        {
            
            F.Beszur(k);
            muvelet(k);

            
            Halmaz<V> szomszedok = g.Szomszedai(k);

            
            szomszedok.Bejar(x =>
            {
                if (!F.Eleme(x))
                {
                    MelysegiBejarasRekurzio(g, x, F, muvelet);
                }
            });
        }

    }
    
    
    
    
    
    
    
    
}

