namespace OE.ALGA.Optimalizalas
{

    public class VisszalepesesKereses<T>
    {
        int n;
        int[] M;
        T[,] R;

        Func<int, T, bool> ft;
        Func<int, T, T[], bool> fk;


        public VisszalepesesKereses(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk)
        {
            this.n = n;
            this.M = M;
            this.R = R;
            this.ft = ft;
            this.fk = fk;
        }

        public T[] OptimalisMegoldas()
        {
            bool van = false;
            T[] E = new T[n];
            T[] O = new T[n];
            Backtrack(0, E, ref van, ref O);
            
            if (van) return O;
            else throw new Exception("A feladatnak nincs optimalis megoldasa!");
        }
        
        void Backtrack(int szint, T[] E, ref bool van, ref T[] O)
        {
            int i = -1;
            while (!van && i < M[szint] - 1)
            {
                i++;
                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == n - 1)
                        {
                            if (!van || Josag(E) > Josag(O))
                            {
                                Array.Copy(E, O, E.Length);
                            }
                            van = true;
                        }
                        
                        else
                        {
                            Backtrack(szint+1, E, ref van, ref O);
                        }
                    }
                }
            }
        }

        private int Josag(T[] solution)
        {
            return 0;
        }
    }
    
}

