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

    private readonly List<IPageViewModel> _pageViewModels = new List<IPageViewModel>();
    private ICommand _changePageCommand;

    private IPageViewModel _currentPageViewModel;
    private bool _showHamburgerMenu = false;

    #endregion Private Members

    #region Public Constructors

    public ApplicationViewModel()
    {
      GameList gameListModel = new GameList();

      // Add available pages
      PageViewModels.Add(new GameListViewModel(gameListModel));
      PageViewModels.Add(new StatsViewModel(new Stats()));
      PageViewModels.Add(new MetadataDownloadViewModel(gameListModel, new MetadataDownload()));

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
          _changePageCommand = new RelayCommand(p => ChangeViewModel((IPageViewModel)p),
                                                p => p is IPageViewModel);
        }

        return _changePageCommand;
      }
    }

    public IPageViewModel CurrentPageViewModel
    {
      get => _currentPageViewModel;
      set => Set(ref _currentPageViewModel, value);
    }

    public List<IPageViewModel> PageViewModels { get => _pageViewModels; }

    public bool ShowHamburgerMenu
    {
      get => _showHamburgerMenu;
      set => Set(ref _showHamburgerMenu, value);
    }

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
  }
}