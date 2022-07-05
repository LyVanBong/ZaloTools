using Prism.Mvvm;
using Prism.Navigation;

namespace ZaloTools.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        protected ViewModelBase()
        {
        }

        public virtual void Destroy()
        {
        }
    }
}