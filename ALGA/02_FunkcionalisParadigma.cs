using OE.ALGA.Paradigmak;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{



    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T>, IEnumerable<T> where T : IVegrehajthato
    {
        public Func<T, bool> BejaroFeltetel
        {
            get
            {
                return BejaroFeltetel;
            }
            set
            {
                BejaroFeltetel = x => true;
            }
            
        }
        
        
    
        
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {

        }

        public new IEnumerator<T> GetEnumerator()
        {
            FeltetelesFeladatTaroloBejaro<T> bejaro = new FeltetelesFeladatTaroloBejaro<T>(BejaroFeltetel, base.tarolo, base.n);
            return bejaro;
        }

        

        public void FeltetelesVegrehajtas(Func<T,bool> feltetel)
        {
            for (int i = 0; i < n; i++)
            {

                if (feltetel.Invoke(tarolo[i]))
                {
                    if (tarolo[i]!=null)
                    {
                        tarolo[i]?.Vegrehajtas();  
                    }
                    
                }
            }
        }
    }

    public class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
    {

        T[] tarolo;
        int n;
        int aktualisIndex = -1;
        public Func<T, bool> Feltetel { get; set; }
        
        


        public FeltetelesFeladatTaroloBejaro(Func<T,bool> feltetel,T[] tarolo, int n  )
        {
            this.tarolo = tarolo;
            this.n = n;
            Feltetel = feltetel;
        }

        public T Current
        {
            get
            {
                if (Feltetel.Invoke(tarolo[aktualisIndex]))
                { 
                    return tarolo[aktualisIndex];
                }
                else 
                {
                    MoveNext();
                    return tarolo[aktualisIndex];
                }
            }
        }
        
        object IEnumerator.Current=>Current;
        public bool MoveNext()
        {
            if (aktualisIndex < n - 1)
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