namespace OE.ALGA.Adatszerkezetek
{
    public class Kupac<T>
    {
        protected T[] E;
        protected int n;
        protected Func<T,T, bool> nagyobbPrioritas;

        public Kupac(T[] e, int n, Func<T, T, bool> nagyobbPrioritas)
        {
            E = e;
            this.n = n;
            this.nagyobbPrioritas = nagyobbPrioritas;
            KupacotEpit();
        }

        public static int Bal(int i) => (i+1) * 2-1;
        public static int Jobb(int i) => (i + 1) * 2;
        
        public static int Szulo(int i) => (int)Math.Floor((double)(i+1) / 2)-1;
        

        protected void Kupacol(int i)
        {
            int jobb = Jobb(i);int max = i; int bal = Bal(i);
            
            if (bal < n && nagyobbPrioritas(E[bal], E[max]))
            {
                max = bal;
            }
            if (jobb < n && nagyobbPrioritas(E[jobb], E[max]))
            {
                max = jobb;
            }
            if(max != i)
            {
                T puff = E[i];
                E[i] = E[max];
                E[max] = puff;
                Kupacol(max);
            }
        }

      
        protected void KupacotEpit()
        {
            for (int i=n/2; i >= 1; i--) 
            {
                Kupacol(i-1);
            }
        }
    }
    //########################################################################
//──────▄▀▄─────▄▀▄
//─────▄█░░▀▀▀▀▀░░█▄
//─▄▄──█░░░░░░░░░░░█──▄▄
//█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█
//########################################################################

    public class KupacRendezes<T> : Kupac<T> where T : IComparable
    {
        public KupacRendezes(T[] e) : base(e, e.Length, (a,b) => a.CompareTo(b) == 1)
        { }

        public void Rendezes()
        {
            for (int i = n-1; i >= 0; i--)
            {
                T puff = E[i];
                E[i] = E[0];
                E[0] = puff;
                n--;
                Kupacol(0);
            }
        }
    } 
    //########################################################################
// ########################───▄▄─▄████▄▐▄▄▄▌##############################
// ########################──▐──████▀███▄█▄▌##############################
// ########################▐─▌──█▀▌──▐▀▌▀█▀ ##############################
// ########################─▀───▌─▌──▐─▌    ##############################
// ########################─────█─█──▐▌█    ##############################
//########################################################################

    public class KupacPrioritasosSor<T> : Kupac<T>, PrioritasosSor<T> 
    {
        public KupacPrioritasosSor(int meret, Func<T,T,bool> prioritas) : base(new T[meret], 0, prioritas)
        {
        }

        public bool Ures { get { return n == 0; } }

        public T Elso()
        {
            if (Ures) { throw new NincsElemKivetel(); }
            return E[0];
        }

        public void Frissit(T elem)
        {
            int i;
            for(i = 0; i < n && !(E[i].Equals(elem));)
            { i++; }
            if(n<=i) throw new NincsElemKivetel();
            KulcsotFelvisz(i);
            Kupacol(i);
        }

        public void KulcsotFelvisz(int i)
        {
            int szulo = Szulo(i);
            if(szulo >= 0 && nagyobbPrioritas(E[i], E[szulo]))
            {
                T puff = E[i];
                E[i] = E[szulo];
                E[szulo] = puff;
                KulcsotFelvisz(szulo);
            }
        }

        public void Sorba(T ertek)
        {
            if (n >= E.Length ) { throw new NincsHelyKivetel(); }
            else
            {
                E[n] = ertek;
                n++;
                KulcsotFelvisz(n-1);
            }
        }

        public T Sorbol()
        {
            if (Ures) throw new NincsElemKivetel();

            T kimenet = E[0];
            E[0] = E[n - 1];
            n--;
            Kupacol(0);
            return kimenet;
        }
    }
}
