﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.ViewModels
{
    public class ContainerViewModel : ElementViewModel
    {
        public List<ElementViewModel> ChildElements { get; set; }

        public ContainerViewModel()
        {
            ChildElements = new List<ElementViewModel>();
        }

        public ContainerViewModel(ExecContainer container, IRuleHandler handler) : this()
        {
            foreach (RuleElement element in container.ChildElements)
            {
                if ((element is IViewModelElement)
                    && (element is ICustomInterpreted) && handler.IsTypeAuthorized(element as ICustomInterpreted))
                {
                    ChildElements.Add((element as IViewModelElement).CreateViewModel(handler));
                }
            }
        }
    }
}