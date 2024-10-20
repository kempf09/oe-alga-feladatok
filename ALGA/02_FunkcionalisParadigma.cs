using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T>, IEnumerable<T> where T : IVegrehajthato
    {
        public Func<T, bool>    BejaroFeltetel
        {
            get; set;
        }
        public bool     valami(T asd)
            {
            return true;
            }


        public      FeltetelesFeladatTarolo(int meret) : base(meret)
        {
        }
        public IEnumerator<T> GetEnumerator()
        {
            FeltetesFeladatTaroloBejaro<T>      bejaro = new     FeltetesFeladatTaroloBejaro<T>(BejaroFeltetel != null ? BejaroFeltetel : x => true, base.tarolo, base.n);
            return bejaro;
        }

        public void       FeltetelesVegrehajtas(Func<T,bool> valami)
        {
            
            for (int i = 0; i < n; i++)
            {
                if     (valami.Invoke(tarolo[i]))
                {
                    if (tarolo[i] != null)      
                        tarolo[i]?.Vegrehajtas();
                }
            }
        }


    }
    public class          FeltetesFeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[]         tarolo;
        int         n;
        int aktualisIndex = -1;
        
        Func<T, bool> feltetel;
        
        public FeltetesFeladatTaroloBejaro(Func<T,bool> feltet, T[] tarolo, int n)
        {
            this.tarolo =            tarolo;
            this.n = n;
            feltetel = feltet;
        }
        public T Current 
        { get 
            {
                
               return            tarolo[aktualisIndex];
                
            } 
        }

        

        object IEnumerator.Current =>            Current;

        public void     Dispose()
        {
        }

        public bool MoveNext()
        {
            while (aktualisIndex++        < n - 1 && !feltetel(tarolo[aktualisIndex]))
            {
            }
            if (aktualisIndex        == n) return false;
            return true;

        }

        public void           Reset()
        {
            aktualisIndex = -1;
        }
    }
}
