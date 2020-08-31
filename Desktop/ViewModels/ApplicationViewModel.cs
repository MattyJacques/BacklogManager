using Desktop.Extensions.Helpers;
using Desktop.Interfaces;
using Desktop.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Desktop.ViewModels
{
  public class ApplicationViewModel : ViewModelBase
  {
    #region Private Members

    private ICommand _changePageCommand;

    private IPageViewModel _currentPageViewModel;
    private List<IPageViewModel> _pageViewModels;
    private bool _showHamburgerMenu = false;

    #endregion Private Members

    #region Public Constructors

    public ApplicationViewModel()
    {
      // Add available pages
      PageViewModels.Add(new GameListViewModel(new GameList()));
      PageViewModels.Add(new StatsViewModel(new Stats()));

      // Set starting page
      CurrentPageViewModel = PageViewModels[0];
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Command to change to the view of the window
    /// </summary>
    public ICommand ChangePageCommand
    {
      get
      {
        if (_changePageCommand == null)
        {
          _changePageCommand = new RelayCommand(
              p => ChangeViewModel((IPageViewModel)p),
              p => p is IPageViewModel);
        }

        return _changePageCommand;
      }
    }

    public IPageViewModel CurrentPageViewModel
    {
      get => _currentPageViewModel;
      set
      {
        if (_currentPageViewModel != value)
        {
          _currentPageViewModel = value;
          RaisePropertyChanged("CurrentPageViewModel");
        }
      }
    }

    public List<IPageViewModel> PageViewModels
    {
      get
      {
        if (_pageViewModels == null)
        {
          _pageViewModels = new List<IPageViewModel>();
        }

        return _pageViewModels;
      }
    }

    public bool ShowHamburgerMenu { get => _showHamburgerMenu; set { _showHamburgerMenu = value; RaisePropertyChanged("ShowHamburgerMenu"); } }

    #endregion Public Properties

    #region Private Methods

    /// <summary>
    /// Change the ViewModel to the given type, changing the View
    /// </summary>
    /// <param name="viewModel">View model to change to</param>
    private void ChangeViewModel(IPageViewModel viewModel)
    {
      if (!PageViewModels.Contains(viewModel))
      {
        PageViewModels.Add(viewModel);
      }

      CurrentPageViewModel = PageViewModels
          .FirstOrDefault(vm => vm == viewModel);

      ShowHamburgerMenu = false;
    }

    #endregion Private Methods

    // ChangeViewModel
  }
}