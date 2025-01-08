using CommunityToolkit.Mvvm.Input;
using MobileAppSpring2025.Models;

namespace MobileAppSpring2025.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}