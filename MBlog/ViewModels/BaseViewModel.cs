using MBlog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DataTest> listData;
        public ObservableCollection<DataTest> ListData
        {
            get { return listData; }
            set
            {
                if (!Equals(listData, value))
                {
                    listData = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<DataTest> listDataTest;
        public ObservableCollection<DataTest> ListDataTest
        {
            get { return listDataTest; }
            set
            {
                if (!Equals(listDataTest, value))
                {
                    listDataTest = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<DataTest> listDataTop3;
        public ObservableCollection<DataTest> ListDataTop3
        {
            get { return listDataTop3; }
            set
            {
                if (!Equals(listDataTop3, value))
                {
                    listDataTop3 = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        private bool loadingForyou;
        public bool LoadingForyou
        {
            get { return loadingForyou; }
            set
            {
                if (!Equals(loadingForyou, value))
                {
                    loadingForyou = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool loadingTopic;
        public bool LoadingTopic
        {
            get { return loadingTopic; }
            set
            {
                if (!Equals(loadingTopic, value))
                {
                    loadingTopic = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool loadingHot;
        public bool LoadingHot
        {
            get { return loadingHot; }
            set
            {
                if (!Equals(loadingHot, value))
                {
                    loadingHot = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool loadingLatest;
        public bool LoadingLatest
        {
            get { return loadingLatest; }
            set
            {
                if (!Equals(loadingLatest, value))
                {
                    loadingLatest = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool loadingFollowing;
        public bool LoadingFollowing
        {
            get { return loadingFollowing; }
            set
            {
                if (!Equals(loadingFollowing, value))
                {
                    loadingFollowing = value;
                    OnPropertyChanged();
                }
            }
        }
        public BaseViewModel()
        {
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearErrorMessage()
        {
            ErrorMessage = "";
        }
        public async Task MockDatatest()
        {
            LoadingForyou = true;
            LoadingTopic = true;
            LoadingLatest = true;
            LoadingFollowing = true;
            LoadingHot = true;
            await Task.Delay(1000);
            ListData = new ObservableCollection<DataTest>()
              {
                      new DataTest
                      {
                          Title = "American Black Bear ttttttttttttttttttt",
                          BookmarkShow=true,
                          Expire= DateTime.Now,
                          Detail = "The American black bear is a medium-sized bear native to North America. It is the continent's smallest and most widely distributed bear species. American black bears are omnivores, with their diets varying greatly depending on season and Expire. They typically live in largely forested areas, but do leave forests in search of food. Sometimes they become attracted to human communities because of the immediate availability of food. The American black bear is the world's most common bear species.",
                          Image ="javascript96crop.png",
                          TopicName="Foods"
                          //Image = "https://upload.wikimedia.org/wikipedia/commons/0/08/01_Schwarzbär.jpg"
                      },
                      new DataTest
                      {
                          Title = "Asian Black Bear",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "The Asian black bear, also known as the moon bear and the white-chested bear, is a medium-sized bear species native to Asia and largely adapted to arboreal life. It lives in the Himalayas, in the northern parts of the Indian subcontinent, Korea, northeastern China, the Russian Far East, the Honshū and Shikoku islands of Japan, and Taiwan. It is classified as vulnerable by the International Union for Conservation of Nature (IUCN), mostly because of deforestation and hunting for its body parts.",
                          //Image ="duck.jpg"
                          TopicName="Tech",
                          Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/Ursus_thibetanus_3_%28Wroclaw_zoo%29.JPG/180px-Ursus_thibetanus_3_%28Wroclaw_zoo%29.JPG"))
                      },
                      new DataTest
                      {
                          Title = "Brown Bear",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "The brown bear is a bear that is found across much of northern Eurasia and North America. In North America the population of brown bears are often called grizzly bears. It is one of the largest living terrestrial members of the order Carnivora, rivaled in size only by its closest relative, the polar bear, which is much less variable in size and slightly larger on average. The brown bear's principal range includes parts of Russia, Central Asia, China, Canada, the United States, Scandinavia and the Carpathian region, especially Romania, Anatolia and the Caucasus. The brown bear is recognized as a national and state animal in several European countries.",
                          //Image ="duck.jpg"
                          TopicName="HOT",
                          Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/5/5d/Kamchatka_Brown_Bear_near_Dvuhyurtochnoe_on_2015-07-23.jpg/320px-Kamchatka_Brown_Bear_near_Dvuhyurtochnoe_on_2015-07-23.jpg"))
                      },
                      new DataTest
                      {
                          Title = "Grizzly-Polar Bear Hybrid",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "A grizzly–polar bear hybrid is a rare ursid hybrid that has occurred both in captivity and in the wild. In 2006, the occurrence of this hybrid in nature was confirmed by testing the DNA of a unique-looking bear that had been shot near Sachs Harbour, Northwest Territories on Banks Island in the Canadian Arctic. The number of confirmed hybrids has since risen to eight, all of them descending from the same female polar bear.",
                          //Image ="duck.jpg"
                          Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/7/7e/Grolar.JPG/276px-Grolar.JPG"))
                      },
                      new DataTest
                      {
                          Title = "Sloth Bear",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "The sloth bear is an insectivorous bear species native to the Indian subcontinent. It is listed as Vulnerable on the IUCN Red List, mainly because of habitat loss and degradation. It has also been called labiated bear because of its long lower lip and palate used for sucking insects. Compared to brown and black bears, the sloth bear is lankier, has a long, shaggy fur and a mane around the face, and long, sickle-shaped claws. It evolved from the ancestral brown bear during the Pleistocene and through convergent evolution shares features found in insect-eating mammals.",
                          Image ="duck.jpg"
                          // Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Sloth_Bear_Washington_DC.JPG/320px-Sloth_Bear_Washington_DC.JPG"))
                      },
                      new DataTest
                      {
                          Title = "Sun Bear",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "The sun bear is a bear species occurring in tropical forest habitats of Southeast Asia. It is listed as Vulnerable on the IUCN Red List. The global population is thought to have declined by more than 30% over the past three bear generations. Suitable habitat has been dramatically reduced due to the large-scale deforestation that has occurred throughout Southeast Asia over the past three decades. The sun bear is also known as the honey bear, which refers to its voracious appetite for honeycombs and honey.",
                           Image ="javascript96crop.png"
                          //Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Sitting_sun_bear.jpg/319px-Sitting_sun_bear.jpg"))
                      },
                      new DataTest
                      {
                          Title = "Polar Bear",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "The polar bear is a hypercarnivorous bear whose native range lies largely within the Arctic Circle, encompassing the Arctic Ocean, its surrounding seas and surrounding land masses. It is a large bear, approximately the same size as the omnivorous Kodiak bear. A boar (adult male) weighs around 350–700 kg (772–1,543 lb), while a sow (adult female) is about half that size. Although it is the sister species of the brown bear, it has evolved to occupy a narrower ecological niche, with many body characteristics adapted for cold temperatures, for moving across snow, ice and open water, and for hunting seals, which make up most of its diet. Although most polar bears are born on land, they spend most of their time on the sea ice. Their scientific Title means maritime bear and derives from this fact. Polar bears hunt their preferred food of seals from the edge of sea ice, often living off fat reserves when no sea ice is present. Because of their dependence on the sea ice, polar bears are classified as marine mammals.",
                          Image ="duck.jpg"
                         // Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/6/66/Polar_Bear_-_Alaska_%28cropped%29.jpg"))
                      },
                      new DataTest
                      {
                          Title = "Spectacled Bear",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "The spectacled bear, also known as the Andean bear or Andean short-faced bear and locally as jukumari (Aymara), ukumari (Quechua) or ukuku, is the last remaining short-faced bear. Its closest relatives are the extinct Florida spectacled bear, and the giant short-faced bears of the Middle to Late Pleistocene age. Spectacled bears are the only surviving species of bear native to South America, and the only surviving member of the subfamily Tremarctinae. The species is classified as Vulnerable by the IUCN because of habitat loss.",
                          Image ="duck.jpg"
                         // Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Spectacled_Bear_-_Houston_Zoo.jpg/264px-Spectacled_Bear_-_Houston_Zoo.jpg"))
                      },
                      new DataTest
                      {
                          Title = "Short-faced Bear",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "The short-faced bears is an extinct bear genus that inhabited North America during the Pleistocene epoch from about 1.8 Mya until 11,000 years ago. It was the most common early North American bear and was most abundant in California. There are two recognized species: Arctodus pristinus and Arctodus simus, with the latter considered to be one of the largest known terrestrial mammalian carnivores that has ever existed. It has been hypothesized that their extinction coincides with the Younger Dryas period of global cooling commencing around 10,900 BC.",
                           Image ="duck.jpg"
                          //Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/b/b8/ArctodusSimusSkeleton.jpg/320px-ArctodusSimusSkeleton.jpg"))
                      },
                      new DataTest
                      {
                          Title = "California Grizzly Bear",
                          BookmarkShow=true,
                          Expire = DateTime.Now,
                          Detail = "The California grizzly bear is an extinct subspecies of the grizzly bear, the very large North American brown bear. Grizzly could have meant grizzled (that is, with golden and grey tips of the hair) or fear-inspiring. Nonetheless, after careful study, naturalist George Ord formally classified it in 1815 – not for its hair, but for its character – as Ursus horribilis (terrifying bear). Genetically, North American grizzlies are closely related; in size and coloring, the California grizzly bear was much like the grizzly bear of the southern coast of Alaska. In California, it was particularly admired for its beauty, size and strength. The grizzly became a symbol of the Bear Flag Republic, a moniker that was attached to the short-lived attempt by a group of American settlers to break away from Mexico in 1846. Later, this rebel flag became the basis for the state flag of California, and then California was known as the Bear State.",
                          Image ="duck.jpg"
                          // Image = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/d/de/Monarch_the_bear.jpg"))
                      }
                  };
           var ListDataTe = ListData.Select(d => new DataTest()
            {
                BookmarkShow=false,
                Detail=d.Detail,
                Expire=d.Expire,
                Image=d.Image,
                Title=d.Title
            }).ToList();
            this.ListDataTest = new ObservableCollection<DataTest>(ListDataTe);
            ListDataTop3 = new ObservableCollection<DataTest>();
            for (int i = 0; i < 3; i++)
            {
                ListDataTop3.Add(ListData[i]);
            }
            LoadingForyou = false;
            await Task.Delay(500);
            LoadingTopic = false;
            await Task.Delay(500);
            LoadingLatest = false;
            await Task.Delay(500); 
            LoadingHot = false;
            await Task.Delay(500);
            LoadingFollowing = false;
        }
    }
}
