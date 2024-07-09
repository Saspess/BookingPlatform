using BP.Utils.Models.EnumOption;

namespace BP.Utils.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<EnumOptionViewModel> GetEnumOptions<T>()
        {
            var resolutionTypeViewModels = new List<EnumOptionViewModel>();

            var enumValues = Enum.GetValues(typeof(T));

            foreach (var value in enumValues)
            {
                var enumOptionViewModel = new EnumOptionViewModel
                {
                    Id = (int)value,
                    Text = Enum.GetName(typeof(T), value).ToString()
                };

                resolutionTypeViewModels.Add(enumOptionViewModel);
            }

            return resolutionTypeViewModels;
        }
    }
}
