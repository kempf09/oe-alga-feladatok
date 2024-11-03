namespace OE.ALGA.Optimalizalas
{

    public class DinamikusHatizsakPakolas
    {
        HatizsakProblema problema;
        public int LepesSzam { get; private set; }

        public DinamikusHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
            this.LepesSzam = 0;
        }


        public float[,] TablazatFeltoltes()
        {
            float[,] tablazat = new float[problema.n+1, problema.Wmax+1];
            int n=problema.n;
            int Wmax=problema.Wmax;

            for (int t = 1; t < n; t++)
            {
                for (int h = 0; h <= Wmax; h++)
                {
                    LepesSzam++;
                    if (h<problema.w[t-1])
                    {
                        tablazat[t, h] = tablazat[t-1, h];
                    }
                    else
                    {
                        tablazat[t, h] = Math.Max(tablazat[t - 1, h], tablazat[t - 1, h - problema.w[t - 1]] + problema.p[t - 1]);
                    }
                }
                
            }
                
            return tablazat;
            
        }

        public float OptimalisErtek()
        {
            float [,] F=TablazatFeltoltes();
            return F[problema.n, problema.Wmax];
        }

        
        public bool[] OptimalisMegoldas()
        {
            float[,] F = TablazatFeltoltes();
            int n = problema.n;
            int Wmax = problema.Wmax;

            // Optimális megoldás visszakövetése
            bool[] megoldas = new bool[n];
            int h = Wmax;

            for (int t = n; t > 0 && h > 0; t--)
            {
                // Megnézzük, hogy a t-ik tárgyat beletettük-e az optimális megoldásba
                if (F[t, h] != F[t - 1, h])
                {
                    megoldas[t - 1] = true; // A t-ik tárgyat beletettük
                    h -= problema.w[t - 1]; // Csökkentjük a súlykapacitást
                }
            }

            return megoldas;
        }



    }
    
    
    
    
    
    
    
}

