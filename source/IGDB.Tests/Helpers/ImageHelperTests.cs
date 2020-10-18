using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IGDB.Tests.Helpers
{
  [TestClass]
  public class ImageHelperTests
  {
    #region Private Members

    private const string _testImageId = "abcxyz123";

    #endregion Private Members

    #region Public Methods

    [TestMethod]
    public void ShouldDefaultToThumbnailNoRetina()
    {
      var imageUrl = ImageHelper.GetImageUrl(_testImageId);

      Assert.IsTrue(imageUrl.Contains("/t_thumb/"));
    }

    [TestMethod]
    public void ShouldSupport1080p()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.HD1080).Contains("/t_1080p/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.HD1080, true).Contains("/t_1080p_2x/"));
    }

    [TestMethod]
    public void ShouldSupport720p()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.HD720).Contains("/t_720p/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.HD720, true).Contains("/t_720p_2x/"));
    }

    [TestMethod]
    public void ShouldSupportCoverBig()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.CoverBig).Contains("/t_cover_big/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.CoverBig, true).Contains("/t_cover_big_2x/"));
    }

    [TestMethod]
    public void ShouldSupportCoverSmall()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.CoverSmall).Contains("/t_cover_small/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.CoverSmall, true).Contains("/t_cover_small_2x/"));
    }

    [TestMethod]
    public void ShouldSupportLogoMed()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.LogoMed).Contains("/t_logo_med/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.LogoMed, true).Contains("/t_logo_med_2x/"));
    }

    [TestMethod]
    public void ShouldSupportMicro()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.Micro).Contains("/t_micro/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.Micro, true).Contains("/t_micro_2x/"));
    }

    [TestMethod]
    public void ShouldSupportScreenshotBig()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.ScreenshotBig).Contains("/t_screenshot_big/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.ScreenshotBig, true).Contains("/t_screenshot_big_2x/"));
    }

    [TestMethod]
    public void ShouldSupportScreenshotHuge()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.ScreenshotHuge).Contains("/t_screenshot_huge/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.ScreenshotHuge, true).Contains("/t_screenshot_huge_2x/"));
    }

    [TestMethod]
    public void ShouldSupportScreenshotMed()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.ScreenshotMed).Contains("/t_screenshot_med/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.ScreenshotMed, true).Contains("/t_screenshot_med_2x/"));
    }

    [TestMethod]
    public void ShouldSupportThumbnail()
    {
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.Thumb).Contains("/t_thumb/"));
      Assert.IsTrue(ImageHelper.GetImageUrl(_testImageId, ImageSize.Thumb, true).Contains("/t_thumb_2x/"));
    }

    #endregion Public Methods
  }
}