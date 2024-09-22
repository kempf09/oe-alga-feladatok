using OE.ALGA.Paradigmak;

namespace OE.ALGA.Paradigmak
{



    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato
    {
        public Func<T, bool> BejaroFeltetel { get; set; }
        
        
    
        
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {

        }

        

        public void FeltetelesVegrehajtas(Predicate<T> feltetel)
        {
            for (int i = 0; i < n; i++)
            {

                if (feltetel(tarolo[i]))
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
    }

    public class FeltetelesFeladatTaroloBejaro<T> 
    {

        T[] tarolo;
        int n;
        int aktualisIndex = -1;
        public Func<T, bool> Feltetel { get; set; }
        
        


        public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Func<T,bool> feltetel )
        {
            this.tarolo = tarolo;
            this.n = n;
            Feltetel = feltetel;
        }

        public T Current
        {
            get { return tarolo[aktualisIndex]; }
        }

        public bool MoveNext()
        {
            if (aktualisIndex < n - 1 && Feltetel(tarolo[aktualisIndex]))
            {
                aktualisIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }

        

        public void Dispose()
        {

        }
    }
















}        