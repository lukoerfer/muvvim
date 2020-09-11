using System;

namespace Muvvim.ViewModel
{
    public abstract class ViewModel<ModelType> : ViewModel
    {
        protected readonly ModelType Model;

        public ViewModel()
        {
            Model = Activator.CreateInstance<ModelType>();
        }

        public ViewModel(ModelType model)
        {
            Model = model;
        }

    }
}
