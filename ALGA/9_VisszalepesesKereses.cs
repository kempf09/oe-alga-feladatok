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
        public int LepesSzam { get; private set; }


        public VisszalepesesOptimalizacio(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], int> josag)
        {
            this.n = n; this.M = M; this.R = R; this.ft = ft; this.fk = fk; this.josag = josag;
            LepesSzam = 0;
        }


        public T[] OptimalisMegoldas()
        {
            bool van = false;
            T[] E = new T[n];
            T[]O=new T[n];
            Backtrack(0,ref E, ref van, ref O);
            if (van){ return O;}
            else {throw new InvalidOperationException("Nincs valid megoldas");}
            
        }

        public void Backtrack(int szint, ref T[] E, ref bool van, ref T[]O)
        {
            int i = -1;
            while (!van && i < M[szint] - 1)
            {
                i++;
                if (ft(szint,R[szint,i]))
                {
                    if (fk(szint,R[szint,i],E))
                    {
                        E[szint]=R[szint,i];
                        if (szint == n - 1)
                        {
                            if (!van || josag(E) > josag(O))
                            {
                                Array.Copy(E,O,E.Length);
                                van = true;
                            }
                            
                            LepesSzam++;
                        }
                        else
                        {
                            Backtrack(szint+1, ref E, ref van,ref O);
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
        public int LepesSzam { get; private set; }
        

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
            
            var opt= new VisszalepesesOptimalizacio<bool>(
                n,
                M,
                R,
                (szint,value)=>true,
                (szint, value, E) =>
                {
                    E[szint]=value;
                    return problema.Ervenyes(E);

                },
                E=>(int)problema.OsszErtek(E)
                
                ); 
            
            bool[]result=opt.OptimalisMegoldas();
            
            LepesSzam=opt.LepesSzam;
            
            return result;
        }

        public float OptimalisErtek()
        {
            bool[]optMeg=OptimalisMegoldas();
            return problema.OsszErtek(optMeg);
        }
        
        


    }
    
    
    
    
    
    
    
    
    
    
    
}