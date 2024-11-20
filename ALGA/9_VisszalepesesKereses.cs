namespace OE.ALGA.Optimalizalas
{


    public class VisszalepesesOptimalizacio<T>
    {
        public int n;
        public int[] M;
        public T[,] R;
        public Func<int, T, bool> ft;
        public Func<int, T, T[], bool> fk;
        public Func<T[], int> josag;
        public int LepesSzam { get; set; }


        public VisszalepesesOptimalizacio(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk,
            Func<T[], int> josag)
        {
            this.n = n;
            this.M = M;
            this.R = R;
            this.ft = ft;
            this.fk = fk;
            this.josag = josag;
            LepesSzam = 0;
        }


        public T[] OptimalisMegoldas()
        {
            bool van = false;
            T[] E = new T[n];
            T[] O = new T[n];
            Backtrack(0, ref E, ref van,  ref O);
            if (van)
            {
                return O;
            }
            else
            {
                throw new InvalidOperationException("Nincs valid megoldas");
            }

        }

        public void Backtrack(int szint, ref T[] E, ref bool van, ref T[] O)
        {
            int i = -1;
            while (i < M[szint] - 1) // nem kell a !van
            {
                i++;
                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == n - 1)
                        {
                            if (!van || josag(E) > josag(O))
                            {
                                E.CopyTo(O, 0);

                            }

                            van = true;

                            LepesSzam++;
                        }
                        else
                        {
                            Backtrack(szint + 1,ref E, ref van, ref O);
                        }

                    }

                }
            }
        }


    }
    //########################################################################


    public class VisszalepesesHatizsakPakolas
    {
        public HatizsakProblema problema;
        public int LepesSzam { get; set; }


        public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
            LepesSzam = 0;
        }



        public bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = new int[n];
            bool[,] R = new bool[n, 2];

            for (int i = 0; i < n; i++)
            {
                M[i] = 2;
                R[i, 0] = true;
                R[i, 1] = false;
            }

            var opt = new VisszalepesesOptimalizacio<bool>(
                n,
                M,
                R,
                (szint, value) => true,
                (szint, value, E) =>
                {
                    E[szint] = value;
                    return problema.Ervenyes(E);

                },
                E => (int)problema.OsszErtek(E)

            );

            bool[] result = opt.OptimalisMegoldas();

            LepesSzam = opt.LepesSzam;

            return result;
        }

        public float OptimalisErtek()
        {
            bool[] optMeg = OptimalisMegoldas();
            return problema.OsszErtek(optMeg);
        }
    }


    //#################################################################################
    public class SzetvalasztasEsKorlatozasOptimalizacio<T> : VisszalepesesOptimalizacio<T>
    {

        public Func<int, T[], int> fb;


        public SzetvalasztasEsKorlatozasOptimalizacio(
            int n,
            int[] M,
            T[,] R,
            Func<int, T, bool> ft,
            Func<int, T, T[], bool> fk,
            Func<T[], int> josag,
            Func<int, T[], int> fb)
            : base(n, M, R, ft, fk, josag)
        {
            this.fb = fb;



        }

        public void Backtrack(int szint, T[] E, ref bool van, T[] O)
        {

            int i = -1;

            while (!van && i < M[szint] - 1)
            {
                i++;
                T candidate = R[szint, i];

                if (ft(szint, candidate) && fk(szint, candidate, E))
                {
                    E[szint] = candidate;

                    if (szint == n - 1) 
                    {
                        int currentSolutionValue = josag(E);
                        if (!van || currentSolutionValue > josag(O))
                        {
                            E.CopyTo(O, 0);
                            van = true;
                        }

                        LepesSzam++;
                    }
                    else
                    {
                        
                        int currentEstimate = josag(E) + fb(szint, E);
                        if (!van || currentEstimate > josag(O))
                        {
                            Backtrack(szint + 1, E, ref van, O);
                        }
                    }
                }
            }
        }
    }

    //##########################################£
    public class SzetvalasztasEsKorlatozasHatizsakPakolas : VisszalepesesHatizsakPakolas
    {
        
        public SzetvalasztasEsKorlatozasHatizsakPakolas(HatizsakProblema problema) : base(problema)
        {
        }


        public bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = new int[n];
            bool[,] R = new bool[n, 2];


            for (int i = 0; i < n; i++)
            {
                M[i] = 2;
                R[i, 0] = true; 
                R[i, 1] = false; 
            }


            var opt = new SzetvalasztasEsKorlatozasOptimalizacio<bool>(
                n,
                M,
                R,
                (szint, value) => true,
                (szint, value, E) =>
                {
                    E[szint] = value;
                    return problema.Ervenyes(E);
                },
                E => (int)problema.OsszErtek(E),
                (szint, E) =>
                {
                    int remainingCapacity = problema.Wmax;

                    for (int i = 0; i < szint; i++)
                    {
                        if (E[i])
                        {
                            remainingCapacity -= problema.w[i];
                        }
                    }


                    if (remainingCapacity <= 0)
                    {
                        return 0;
                    }


                    float bestValuePerWeight = 0;

                    for (int i = 0; i < problema.n; i++)
                    {
                        if (!E[i])
                        {
                            float valuePerWeight = problema.p[i] / problema.w[i];
                            if (valuePerWeight > bestValuePerWeight)
                            {
                                bestValuePerWeight = valuePerWeight;
                            }
                        }
                    }


                    return (int)(remainingCapacity * bestValuePerWeight);
                }
            );

            bool[] result = opt.OptimalisMegoldas();


            LepesSzam = opt.LepesSzam;

            return result;
        }
    }
}



