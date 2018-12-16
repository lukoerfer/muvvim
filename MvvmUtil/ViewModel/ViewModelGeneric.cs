using MvvmUtil.PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MvvmUtil.ViewModel
{
    public abstract class ViewModel<ModelType> : ViewModel
    {
        protected ModelType Model;

        public ViewModel()
        {
            this.Model = Activator.CreateInstance<ModelType>();
        }

        public ViewModel(ModelType model)
        {
            this.Model = model;
        }

    }
}
