using System.Collections.Generic;

namespace OE.ALGA.Adatszerkezetek
{
    public class FaElem<T> where T : IComparable
    {
        public T tart;
        public FaElem<T>? bal;
        public FaElem<T>? jobb;

        public FaElem(T tart, FaElem<T>? bal, FaElem<T>? jobb)
        {
            this.tart = tart;
            this.bal = bal;
            this.jobb = jobb;
        }
    }
/// <summary>
/// FAHALMAZ - FAHALMAZ - FAHALMAZ - FAHALMAZ - FAHALMAZ - FAHALMAZ - 
/// </summary>
/// <typeparam name="T"></typeparam>
    public class FaHalmaz<T> : Halmaz<T> where T : IComparable
    {
        public FaElem<T>? gyoker;
        
        static FaElem<T> ReszFaBeszur(FaElem<T>? p, T ertek)
        {
            if (p == null)
            {
                FaElem<T> uj=new FaElem<T>(ertek,null,null);
                return uj;
            }
            else
            {
                if (p.tart.CompareTo(ertek)>0)
                {
                    p.bal=ReszFaBeszur(p.bal,ertek);
                }
                else
                {
                    if (p.tart.CompareTo(ertek) < 0)
                    {
                        p.jobb=ReszFaBeszur(p.jobb,ertek);
                    }
                } 
                return p;
            }
        }
        
        public void Beszur(T ertek)
        {
           gyoker= ReszFaBeszur(gyoker, ertek);
        }

        static bool ReszFaEleme(FaElem<T>? p, T ertek)
        {
            if (p != null)
            {
                if (p.tart.CompareTo(ertek) > 0) return ReszFaEleme(p.bal, ertek);
                else
                {
                    if (p.tart.CompareTo(ertek) < 0) return ReszFaEleme(p.jobb, ertek);
                    else return true;
                }
            }
            else return false;
        }
        
        
        public bool Eleme(T ertek) 
        {
            return ReszFaEleme(gyoker, ertek);
        }


        static FaElem<T> KetGyerekesTorles(FaElem<T>? e, FaElem<T>? r)
        {
            
            FaElem<T>? q;
            if (r.jobb!=null)
            {
                r.jobb=KetGyerekesTorles(e,r.jobb );
                return r;
            }
            else
            {
                e.tart=r.tart;
                q = r.bal;
                return q;
            }
        }


        static FaElem<T> ReszfabolTorol(FaElem<T>? p, T ertek)
        {
            FaElem<T>? q;
            if (p != null)
            {
                if (p.tart.CompareTo(ertek) > 0) p.bal = ReszfabolTorol(p.bal, ertek);
                else
                {
                    if (p.tart.CompareTo(ertek) < 0) p.jobb = ReszfabolTorol(p.jobb, ertek);
                    else
                    {
                        if (p.bal == null)
                        {
                            q = p;
                            p = p.jobb;
                        }
                        else
                        {
                            if (p.jobb == null)
                            {
                                q = p;
                                p = p.bal;
                            }
                            else
                            {
                                p.bal = KetGyerekesTorles(p, p.bal);
                            }
                        }
                    }
                }
                return p;
            }
            else throw new NincsElemKivetel();
        }
        
        
        
        public void Torol(T ertek)
        {
            gyoker=ReszfabolTorol(gyoker, ertek);
        }

        
        
        
        static void ReszfaBejarasPreOrder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p!=null)
            {
                muvelet(p.tart);
                ReszfaBejarasPreOrder(p.bal, muvelet);
                ReszfaBejarasPreOrder(p.jobb, muvelet);
            }
        }
        
        static void ReszfaBejarasInOrder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p!=null)
            {
                ReszfaBejarasPreOrder(p.bal, muvelet);
                muvelet(p.tart);
                ReszfaBejarasPreOrder(p.jobb, muvelet);
            }
        }
        
        static void ReszfaBejarasPostOrder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p!=null)
            {
                ReszfaBejarasPreOrder(p.bal, muvelet);
                ReszfaBejarasPreOrder(p.jobb, muvelet);
                muvelet(p.tart);
            }
        }
        
        
        
        public void Bejar(Action<T> muvelet)
        {
            ReszfaBejarasPreOrder(gyoker, muvelet);
        }
    }
}

