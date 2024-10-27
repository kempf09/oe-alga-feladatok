namespace OE.ALGA.Optimalizalas
{
    public class HatizsakProblema
    {
        public int n { get; }// pakolhato targyak szama
        public int Wmax { get; }//hatizsak merete
        private readonly int[] w; // targyak sulyai
        private readonly float[] p; // targyak ertekei

        public HatizsakProblema(int n, int Wmax, int[] w, float[] p)
        {
            this.n = n;
            this.Wmax = Wmax;
            this.w = w;
            this.p = p;
        }

        public float OsszErtek(bool[]X )
        {
            if (X == null) return 0;
            else
            {
                float s = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    if (X[i])
                    {
                        s = s + p[i];
                    }
                }

                return s;  
            }
            
        }

        public float OsszSuly(bool[]X)
        {
            float s = 0;
            for (int i = 0; i < n; i++)
            {
                if (X[i]) s=s+w[i];
            }

            return s;
        }


        public bool Ervenyes(bool[] X)
        {
            if (OsszSuly(X) <= Wmax) return true;
            
            
            else return false;
        }
        
    }
    
    //#######################################################################
    public class NyersEro<T>
    {
        int m;
        private Func<int, T> generator;
        private Func<T, float> josag;
        public int Lepesszam { get; private set; }

        public NyersEro(int m, Func<int, T> generator, Func<T, float> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
            
        }

        public T OptimalisMegoldas()
        {
            T o = generator(1);
            for (int i = 2; i <=m; i++)
            {
                T x = generator(i);
                Lepesszam++;
                if (josag(x)> josag(o))
                {
                    o = x;
                }
            }

            return o;
        }

    }
    //#######################################################################
    public class NyersEroHatizsakPakolas
    {
        public int LepesSzam { get; private set; }
        public HatizsakProblema problema;

        // segitoertekek
        private bool[] optimPakolas;
        private float optimErtek;
    
        public NyersEroHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema ?? throw new ArgumentNullException(nameof(problema));
            this.LepesSzam = 0;
        }

        // Generátor függvény
        public bool[] Generator(int i)
        {
            bool[] K = new bool[problema.n];
            for (int j = 0; j < problema.n; j++)
            {
                K[j] = (i & (1 << j)) != 0;
            }
            return K;
        }

        // Jóság függvény
        public float Josag(bool[] pakolas)
        {
            return problema.Ervenyes(pakolas) ? problema.OsszErtek(pakolas) : -1;
        }
        
        public bool[] OptimalisMegoldas()
        {
            int m = (int)Math.Pow(2, problema.n);

           
            var opt = new NyersEro<bool[]>(m, Generator, Josag);
            optimPakolas = opt.OptimalisMegoldas();
            LepesSzam = opt.Lepesszam;  

            optimErtek = Josag(optimPakolas);  // optErtek tárolása

            return optimPakolas;
        }

        // optErtek visszaadasa
        public float OptimalisErtek()
        {
            if (optimPakolas == null)
            {
                //fixen lefut-e elotte???
                OptimalisMegoldas();
            }
            return optimErtek;
        }
    }

    
    
}

