using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace readShorts.BusinessLogic.ServiceBL
{
    public class IpDetectionBL
    {
        private class Ip2Country
        {
            public ulong BeginIpNumber { get; set; }
            public ulong EndIpNumber { get; set; }
            public string CountryName { get; set; }

            public Ip2Country(ulong beginIpNumber, ulong endIpNumber, string countryName)
            {
                BeginIpNumber = beginIpNumber;
                EndIpNumber = endIpNumber;
                CountryName = countryName;
            }
        }

        private static List<Ip2Country> _dict;

        public static bool IsIsraelIP(string ipAddress)
        {
            if (_dict == null)
            {
                _dict = new List<Ip2Country>();
                CreateIpNumberDict();
            }

            bool result = false;

            if (!string.IsNullOrEmpty(ipAddress))
            {
                char[] del = { '.' };
                string[] ip = ipAddress.Split(del);
                uint w, x, y, z;

                if (ip != null && ip.Length == 4)
                {
                    bool resultx = UInt32.TryParse(ip[0], out w);
                    bool resulty = UInt32.TryParse(ip[1], out x);
                    bool resultz = UInt32.TryParse(ip[2], out y);
                    bool resultw = UInt32.TryParse(ip[3], out z);
                    if (resultx && resulty && resultz && resultw)
                    {
                        ulong iPNumber = 16777216 * w + 65536 * x + 256 * y + z;
                        foreach (Ip2Country item in _dict)
                        {
                            if (iPNumber >= item.BeginIpNumber && iPNumber <= item.EndIpNumber && item.CountryName == "ISRAEL")
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static void CreateIpNumberDict()
        {
            _dict.Add(new Ip2Country(332132120, 332132127, "ISRAEL"));
            _dict.Add(new Ip2Country(1040187392, 1040252927, "ISRAEL"));
            _dict.Add(new Ip2Country(1043890176, 1043890495, "ISRAEL"));
            _dict.Add(new Ip2Country(1043890592, 1043891775, "ISRAEL"));
            _dict.Add(new Ip2Country(1043891840, 1043892223, "ISRAEL"));
            _dict.Add(new Ip2Country(1043892240, 1043892255, "ISRAEL"));
            _dict.Add(new Ip2Country(1043892288, 1043892479, "ISRAEL"));
            _dict.Add(new Ip2Country(1043893248, 1043893311, "ISRAEL"));
            _dict.Add(new Ip2Country(1043893376, 1043893567, "ISRAEL"));
            _dict.Add(new Ip2Country(1043893584, 1043893759, "ISRAEL"));
            _dict.Add(new Ip2Country(1043894272, 1043894559, "ISRAEL"));
            _dict.Add(new Ip2Country(1043894592, 1043894751, "ISRAEL"));
            _dict.Add(new Ip2Country(1043894784, 1043894847, "ISRAEL"));
            _dict.Add(new Ip2Country(1043894880, 1043895039, "ISRAEL"));
            _dict.Add(new Ip2Country(1043895168, 1043895983, "ISRAEL"));
            _dict.Add(new Ip2Country(1043896000, 1043896063, "ISRAEL"));
            _dict.Add(new Ip2Country(1043896576, 1043897343, "ISRAEL"));
            _dict.Add(new Ip2Country(1043897856, 1043898367, "ISRAEL"));
            _dict.Add(new Ip2Country(1043899648, 1043899903, "ISRAEL"));
            _dict.Add(new Ip2Country(1043900416, 1043905023, "ISRAEL"));
            _dict.Add(new Ip2Country(1043905280, 1043907071, "ISRAEL"));
            _dict.Add(new Ip2Country(1043907584, 1043910143, "ISRAEL"));
            _dict.Add(new Ip2Country(1043910656, 1043912447, "ISRAEL"));
            _dict.Add(new Ip2Country(1043913472, 1043914751, "ISRAEL"));
            _dict.Add(new Ip2Country(1043914880, 1043916799, "ISRAEL"));
            _dict.Add(new Ip2Country(1043917056, 1043917311, "ISRAEL"));
            _dict.Add(new Ip2Country(1043917568, 1043918847, "ISRAEL"));
            _dict.Add(new Ip2Country(1043919872, 1043922943, "ISRAEL"));
            _dict.Add(new Ip2Country(1046085632, 1046151167, "ISRAEL"));
            _dict.Add(new Ip2Country(1046318592, 1046319103, "ISRAEL"));
            _dict.Add(new Ip2Country(1048584192, 1048592383, "ISRAEL"));
            _dict.Add(new Ip2Country(1052250688, 1052250719, "ISRAEL"));
            _dict.Add(new Ip2Country(1052288288, 1052288295, "ISRAEL"));
            _dict.Add(new Ip2Country(1053352032, 1053352039, "ISRAEL"));
            _dict.Add(new Ip2Country(1053353984, 1053354655, "ISRAEL"));
            _dict.Add(new Ip2Country(1053354688, 1053354719, "ISRAEL"));
            _dict.Add(new Ip2Country(1053354752, 1053354831, "ISRAEL"));
            _dict.Add(new Ip2Country(1053354856, 1053354871, "ISRAEL"));
            _dict.Add(new Ip2Country(1053354912, 1053355007, "ISRAEL"));
            _dict.Add(new Ip2Country(1053894784, 1053894815, "ISRAEL"));
            _dict.Add(new Ip2Country(1054539776, 1054605311, "ISRAEL"));
            _dict.Add(new Ip2Country(1074680704, 1074680719, "ISRAEL"));
            _dict.Add(new Ip2Country(1075597408, 1075597439, "ISRAEL"));
            _dict.Add(new Ip2Country(1075599472, 1075599487, "ISRAEL"));
            _dict.Add(new Ip2Country(1076308576, 1076308591, "ISRAEL"));
            _dict.Add(new Ip2Country(1076310464, 1076310527, "ISRAEL"));
            _dict.Add(new Ip2Country(1086359232, 1086359295, "ISRAEL"));
            _dict.Add(new Ip2Country(1087554752, 1087554759, "ISRAEL"));
            _dict.Add(new Ip2Country(1109642368, 1109642383, "ISRAEL"));
            _dict.Add(new Ip2Country(1109642416, 1109642431, "ISRAEL"));
            _dict.Add(new Ip2Country(1125093472, 1125093503, "ISRAEL"));
            _dict.Add(new Ip2Country(1125097408, 1125097439, "ISRAEL"));
            _dict.Add(new Ip2Country(1125097920, 1125097983, "ISRAEL"));
            _dict.Add(new Ip2Country(1125111520, 1125111535, "ISRAEL"));
            _dict.Add(new Ip2Country(1125118656, 1125118687, "ISRAEL"));
            _dict.Add(new Ip2Country(1125119904, 1125119935, "ISRAEL"));
            _dict.Add(new Ip2Country(1125120800, 1125120831, "ISRAEL"));
            _dict.Add(new Ip2Country(1161298304, 1161298311, "ISRAEL"));
            _dict.Add(new Ip2Country(1161312992, 1161312999, "ISRAEL"));
            _dict.Add(new Ip2Country(1296250016, 1296250047, "ISRAEL"));
            _dict.Add(new Ip2Country(1296250080, 1296250111, "ISRAEL"));
            _dict.Add(new Ip2Country(1296252304, 1296252319, "ISRAEL"));
            _dict.Add(new Ip2Country(1299972096, 1300234239, "ISRAEL"));
            _dict.Add(new Ip2Country(1310247168, 1310247183, "ISRAEL"));
            _dict.Add(new Ip2Country(1310247424, 1310247431, "ISRAEL"));
            _dict.Add(new Ip2Country(1310247680, 1310247687, "ISRAEL"));
            _dict.Add(new Ip2Country(1310248448, 1310248455, "ISRAEL"));
            _dict.Add(new Ip2Country(1317132288, 1317132543, "ISRAEL"));
            _dict.Add(new Ip2Country(1336934400, 1337458687, "ISRAEL"));
            _dict.Add(new Ip2Country(1346797568, 1346801663, "ISRAEL"));
            _dict.Add(new Ip2Country(1347051520, 1347059711, "ISRAEL"));
            _dict.Add(new Ip2Country(1348274800, 1348274815, "ISRAEL"));
            _dict.Add(new Ip2Country(1348274928, 1348274935, "ISRAEL"));
            _dict.Add(new Ip2Country(1353842688, 1353933823, "ISRAEL"));
            _dict.Add(new Ip2Country(1353934072, 1353934207, "ISRAEL"));
            _dict.Add(new Ip2Country(1353934272, 1353934519, "ISRAEL"));
            _dict.Add(new Ip2Country(1353934528, 1353934855, "ISRAEL"));
            _dict.Add(new Ip2Country(1353934968, 1353935007, "ISRAEL"));
            _dict.Add(new Ip2Country(1353935104, 1353956607, "ISRAEL"));
            _dict.Add(new Ip2Country(1353957120, 1353957345, "ISRAEL"));
            _dict.Add(new Ip2Country(1353957376, 1353968639, "ISRAEL"));
            _dict.Add(new Ip2Country(1353968896, 1353969151, "ISRAEL"));
            _dict.Add(new Ip2Country(1353969344, 1353969919, "ISRAEL"));
            _dict.Add(new Ip2Country(1353970624, 1353970655, "ISRAEL"));
            _dict.Add(new Ip2Country(1353970704, 1353970943, "ISRAEL"));
            _dict.Add(new Ip2Country(1353971200, 1353971967, "ISRAEL"));
            _dict.Add(new Ip2Country(1353972480, 1353972735, "ISRAEL"));
            _dict.Add(new Ip2Country(1353972992, 1353973247, "ISRAEL"));
            _dict.Add(new Ip2Country(1353973256, 1353973263, "ISRAEL"));
            _dict.Add(new Ip2Country(1353973280, 1353973287, "ISRAEL"));
            _dict.Add(new Ip2Country(1353973416, 1353973423, "ISRAEL"));
            _dict.Add(new Ip2Country(1357250560, 1357316095, "ISRAEL"));
            _dict.Add(new Ip2Country(1358209024, 1358213119, "ISRAEL"));
            _dict.Add(new Ip2Country(1358331904, 1358335999, "ISRAEL"));
            _dict.Add(new Ip2Country(1358598144, 1358602239, "ISRAEL"));
            _dict.Add(new Ip2Country(1358904552, 1358904559, "ISRAEL"));
            _dict.Add(new Ip2Country(1359282176, 1359298559, "ISRAEL"));
            _dict.Add(new Ip2Country(1360459104, 1360459135, "ISRAEL"));
            _dict.Add(new Ip2Country(1361036472, 1361036475, "ISRAEL"));
            _dict.Add(new Ip2Country(1371996160, 1371997183, "ISRAEL"));
            _dict.Add(new Ip2Country(1371998720, 1371999231, "ISRAEL"));
            _dict.Add(new Ip2Country(1372004864, 1372006463, "ISRAEL"));
            _dict.Add(new Ip2Country(1372006496, 1372007167, "ISRAEL"));
            _dict.Add(new Ip2Country(1372007456, 1372007679, "ISRAEL"));
            _dict.Add(new Ip2Country(1372007936, 1372008095, "ISRAEL"));
            _dict.Add(new Ip2Country(1372008192, 1372009983, "ISRAEL"));
            _dict.Add(new Ip2Country(1372010273, 1372010751, "ISRAEL"));
            _dict.Add(new Ip2Country(1372011008, 1372012543, "ISRAEL"));
            _dict.Add(new Ip2Country(1372014592, 1372015615, "ISRAEL"));
            _dict.Add(new Ip2Country(1372016896, 1372017407, "ISRAEL"));
            _dict.Add(new Ip2Country(1372017664, 1372018943, "ISRAEL"));
            _dict.Add(new Ip2Country(1372019456, 1372020735, "ISRAEL"));
            _dict.Add(new Ip2Country(1372022784, 1372023807, "ISRAEL"));
            _dict.Add(new Ip2Country(1372024832, 1372025343, "ISRAEL"));
            _dict.Add(new Ip2Country(1372025600, 1372025663, "ISRAEL"));
            _dict.Add(new Ip2Country(1372025728, 1372028927, "ISRAEL"));
            _dict.Add(new Ip2Country(1372029952, 1372031999, "ISRAEL"));
            _dict.Add(new Ip2Country(1372032256, 1372032511, "ISRAEL"));
            _dict.Add(new Ip2Country(1372035072, 1372037887, "ISRAEL"));
            _dict.Add(new Ip2Country(1372038400, 1372039167, "ISRAEL"));
            _dict.Add(new Ip2Country(1372040192, 1372040703, "ISRAEL"));
            _dict.Add(new Ip2Country(1372041216, 1372041343, "ISRAEL"));
            _dict.Add(new Ip2Country(1372041472, 1372043263, "ISRAEL"));
            _dict.Add(new Ip2Country(1372043776, 1372044415, "ISRAEL"));
            _dict.Add(new Ip2Country(1372044448, 1372044799, "ISRAEL"));
            _dict.Add(new Ip2Country(1372044928, 1372045055, "ISRAEL"));
            _dict.Add(new Ip2Country(1372045088, 1372045119, "ISRAEL"));
            _dict.Add(new Ip2Country(1372045152, 1372045215, "ISRAEL"));
            _dict.Add(new Ip2Country(1372045248, 1372045567, "ISRAEL"));
            _dict.Add(new Ip2Country(1372045888, 1372046559, "ISRAEL"));
            _dict.Add(new Ip2Country(1372046592, 1372047231, "ISRAEL"));
            _dict.Add(new Ip2Country(1372047296, 1372047359, "ISRAEL"));
            _dict.Add(new Ip2Country(1372047616, 1372049663, "ISRAEL"));
            _dict.Add(new Ip2Country(1372050432, 1372061695, "ISRAEL"));
            _dict.Add(new Ip2Country(1373241344, 1373306879, "ISRAEL"));
            _dict.Add(new Ip2Country(1380974592, 1381105663, "ISRAEL"));
            _dict.Add(new Ip2Country(1382449152, 1382465535, "ISRAEL"));
            _dict.Add(new Ip2Country(1386610688, 1386676223, "ISRAEL"));
            _dict.Add(new Ip2Country(1401028608, 1401094143, "ISRAEL"));
            _dict.Add(new Ip2Country(1415446528, 1415577599, "ISRAEL"));
            _dict.Add(new Ip2Country(1416364032, 1416626175, "ISRAEL"));
            _dict.Add(new Ip2Country(1424228352, 1424359423, "ISRAEL"));
            _dict.Add(new Ip2Country(1428148128, 1428148135, "ISRAEL"));
            _dict.Add(new Ip2Country(1430257664, 1430388735, "ISRAEL"));
            _dict.Add(new Ip2Country(1434615808, 1434648575, "ISRAEL"));
            _dict.Add(new Ip2Country(1436524544, 1436526591, "ISRAEL"));
            _dict.Add(new Ip2Country(1442447360, 1442512895, "ISRAEL"));
            _dict.Add(new Ip2Country(1464074240, 1464336383, "ISRAEL"));
            _dict.Add(new Ip2Country(1490917376, 1490919423, "ISRAEL"));
            _dict.Add(new Ip2Country(1490934528, 1490934783, "ISRAEL"));
            _dict.Add(new Ip2Country(1493172224, 1493303295, "ISRAEL"));
            _dict.Add(new Ip2Country(1502216192, 1502347263, "ISRAEL"));
            _dict.Add(new Ip2Country(1507666656, 1507666671, "ISRAEL"));
            _dict.Add(new Ip2Country(1532657664, 1532661759, "ISRAEL"));
            _dict.Add(new Ip2Country(1535598592, 1535602687, "ISRAEL"));
            _dict.Add(new Ip2Country(1536155648, 1536159743, "ISRAEL"));
            _dict.Add(new Ip2Country(1539360768, 1539361791, "ISRAEL"));
            _dict.Add(new Ip2Country(1539376128, 1539377151, "ISRAEL"));
            _dict.Add(new Ip2Country(1539387392, 1539388415, "ISRAEL"));
            _dict.Add(new Ip2Country(1539469824, 1539470335, "ISRAEL"));
            _dict.Add(new Ip2Country(1539547648, 1539548159, "ISRAEL"));
            _dict.Add(new Ip2Country(1539593216, 1539594239, "ISRAEL"));
            _dict.Add(new Ip2Country(1539652608, 1539653631, "ISRAEL"));
            _dict.Add(new Ip2Country(1539662848, 1539663871, "ISRAEL"));
            _dict.Add(new Ip2Country(1539704064, 1539704319, "ISRAEL"));
            _dict.Add(new Ip2Country(1539720704, 1539720959, "ISRAEL"));
            _dict.Add(new Ip2Country(1539735808, 1539736063, "ISRAEL"));
            _dict.Add(new Ip2Country(1539755264, 1539755519, "ISRAEL"));
            _dict.Add(new Ip2Country(1539767808, 1539768063, "ISRAEL"));
            _dict.Add(new Ip2Country(1539775744, 1539775999, "ISRAEL"));
            _dict.Add(new Ip2Country(1539781888, 1539782143, "ISRAEL"));
            _dict.Add(new Ip2Country(1539785984, 1539786239, "ISRAEL"));
            _dict.Add(new Ip2Country(1539788800, 1539789055, "ISRAEL"));
            _dict.Add(new Ip2Country(1539792384, 1539792639, "ISRAEL"));
            _dict.Add(new Ip2Country(1539793664, 1539794175, "ISRAEL"));
            _dict.Add(new Ip2Country(1539798784, 1539799039, "ISRAEL"));
            _dict.Add(new Ip2Country(1540007936, 1540008959, "ISRAEL"));
            _dict.Add(new Ip2Country(1540061184, 1540062207, "ISRAEL"));
            _dict.Add(new Ip2Country(1547558912, 1547563007, "ISRAEL"));
            _dict.Add(new Ip2Country(1559232512, 1559240703, "ISRAEL"));
            _dict.Add(new Ip2Country(1566451712, 1566453759, "ISRAEL"));
            _dict.Add(new Ip2Country(1570590720, 1570592767, "ISRAEL"));
            _dict.Add(new Ip2Country(1571553280, 1571684351, "ISRAEL"));
            _dict.Add(new Ip2Country(2156593152, 2156658687, "ISRAEL"));
            _dict.Add(new Ip2Country(2218786816, 2219769855, "ISRAEL"));
            _dict.Add(new Ip2Country(2324037632, 2324103167, "ISRAEL"));
            _dict.Add(new Ip2Country(2338075392, 2338075647, "ISRAEL"));
            _dict.Add(new Ip2Country(2338084864, 2338085375, "ISRAEL"));
            _dict.Add(new Ip2Country(2338085632, 2338086143, "ISRAEL"));
            _dict.Add(new Ip2Country(2338087424, 2338087679, "ISRAEL"));
            _dict.Add(new Ip2Country(2338108928, 2338109951, "ISRAEL"));
            _dict.Add(new Ip2Country(2338115072, 2338115839, "ISRAEL"));
            _dict.Add(new Ip2Country(2338126592, 2338127615, "ISRAEL"));
            _dict.Add(new Ip2Country(2380398592, 2380464127, "ISRAEL"));
            _dict.Add(new Ip2Country(2476802048, 2476867583, "ISRAEL"));
            _dict.Add(new Ip2Country(2481520640, 2481848319, "ISRAEL"));
            _dict.Add(new Ip2Country(2503016448, 2503081983, "ISRAEL"));
            _dict.Add(new Ip2Country(2780940368, 2780940383, "ISRAEL"));
            _dict.Add(new Ip2Country(3226867968, 3226868223, "ISRAEL"));
            _dict.Add(new Ip2Country(3226884352, 3226884607, "ISRAEL"));
            _dict.Add(new Ip2Country(3228696576, 3228827647, "ISRAEL"));
            _dict.Add(new Ip2Country(3228828160, 3228829183, "ISRAEL"));
            _dict.Add(new Ip2Country(3228829696, 3228830207, "ISRAEL"));
            _dict.Add(new Ip2Country(3228833792, 3228844287, "ISRAEL"));
            _dict.Add(new Ip2Country(3228845568, 3228846335, "ISRAEL"));
            _dict.Add(new Ip2Country(3228846592, 3228847359, "ISRAEL"));
            _dict.Add(new Ip2Country(3228847616, 3228849407, "ISRAEL"));
            _dict.Add(new Ip2Country(3228849664, 3228850175, "ISRAEL"));
            _dict.Add(new Ip2Country(3228850688, 3228851711, "ISRAEL"));
            _dict.Add(new Ip2Country(3228851968, 3228852735, "ISRAEL"));
            _dict.Add(new Ip2Country(3228853248, 3228853567, "ISRAEL"));
            _dict.Add(new Ip2Country(3228853600, 3228854271, "ISRAEL"));
            _dict.Add(new Ip2Country(3228855296, 3228855807, "ISRAEL"));
            _dict.Add(new Ip2Country(3228856064, 3228856319, "ISRAEL"));
            _dict.Add(new Ip2Country(3228857856, 3228858111, "ISRAEL"));
            _dict.Add(new Ip2Country(3228858368, 3228858623, "ISRAEL"));
            _dict.Add(new Ip2Country(3228859648, 3228859903, "ISRAEL"));
            _dict.Add(new Ip2Country(3228860928, 3228861695, "ISRAEL"));
            _dict.Add(new Ip2Country(3228862976, 3228863231, "ISRAEL"));
            _dict.Add(new Ip2Country(3228863488, 3228863743, "ISRAEL"));
            _dict.Add(new Ip2Country(3228864000, 3228864255, "ISRAEL"));
            _dict.Add(new Ip2Country(3228864512, 3228866815, "ISRAEL"));
            _dict.Add(new Ip2Country(3228868096, 3229024255, "ISRAEL"));
            _dict.Add(new Ip2Country(3229938688, 3229938943, "ISRAEL"));
            _dict.Add(new Ip2Country(3229950976, 3229951231, "ISRAEL"));
            _dict.Add(new Ip2Country(3231775744, 3231775999, "ISRAEL"));
            _dict.Add(new Ip2Country(3233629696, 3233629951, "ISRAEL"));
            _dict.Add(new Ip2Country(3234747904, 3234748159, "ISRAEL"));
            _dict.Add(new Ip2Country(3234782720, 3234783999, "ISRAEL"));
            _dict.Add(new Ip2Country(3239088896, 3239089151, "ISRAEL"));
            _dict.Add(new Ip2Country(3239127552, 3239127807, "ISRAEL"));
            _dict.Add(new Ip2Country(3239134208, 3239134463, "ISRAEL"));
            _dict.Add(new Ip2Country(3239135744, 3239135999, "ISRAEL"));
            _dict.Add(new Ip2Country(3239464960, 3239465215, "ISRAEL"));
            _dict.Add(new Ip2Country(3239795712, 3239796223, "ISRAEL"));
            _dict.Add(new Ip2Country(3239877376, 3239877631, "ISRAEL"));
            _dict.Add(new Ip2Country(3240009984, 3240010239, "ISRAEL"));
            _dict.Add(new Ip2Country(3240169472, 3240169983, "ISRAEL"));
            _dict.Add(new Ip2Country(3240225280, 3240225791, "ISRAEL"));
            _dict.Add(new Ip2Country(3240245248, 3240246271, "ISRAEL"));
            _dict.Add(new Ip2Country(3240407040, 3240407295, "ISRAEL"));
            _dict.Add(new Ip2Country(3240460288, 3240461055, "ISRAEL"));
            _dict.Add(new Ip2Country(3240578816, 3240579071, "ISRAEL"));
            _dict.Add(new Ip2Country(3240741376, 3240741631, "ISRAEL"));
            _dict.Add(new Ip2Country(3240742912, 3240743423, "ISRAEL"));
            _dict.Add(new Ip2Country(3240883200, 3240884223, "ISRAEL"));
            _dict.Add(new Ip2Country(3241033728, 3241033983, "ISRAEL"));
            _dict.Add(new Ip2Country(3241125120, 3241125375, "ISRAEL"));
            _dict.Add(new Ip2Country(3241146368, 3241146623, "ISRAEL"));
            _dict.Add(new Ip2Country(3244122112, 3244123135, "ISRAEL"));
            _dict.Add(new Ip2Country(3245130496, 3245130751, "ISRAEL"));
            _dict.Add(new Ip2Country(3245134592, 3245134847, "ISRAEL"));
            _dict.Add(new Ip2Country(3245167104, 3245167359, "ISRAEL"));
            _dict.Add(new Ip2Country(3247070208, 3247070463, "ISRAEL"));
            _dict.Add(new Ip2Country(3247347456, 3247347711, "ISRAEL"));
            _dict.Add(new Ip2Country(3249724416, 3249724671, "ISRAEL"));
            _dict.Add(new Ip2Country(3250192896, 3250193151, "ISRAEL"));
            _dict.Add(new Ip2Country(3251117568, 3251117823, "ISRAEL"));
            _dict.Add(new Ip2Country(3251215232, 3251215359, "ISRAEL"));
            _dict.Add(new Ip2Country(3251357184, 3251357695, "ISRAEL"));
            _dict.Add(new Ip2Country(3252584704, 3252584959, "ISRAEL"));
            _dict.Add(new Ip2Country(3253648384, 3253649407, "ISRAEL"));
            _dict.Add(new Ip2Country(3253653504, 3253654527, "ISRAEL"));
            _dict.Add(new Ip2Country(3253693440, 3253694463, "ISRAEL"));
            _dict.Add(new Ip2Country(3253974912, 3253974975, "ISRAEL"));
            _dict.Add(new Ip2Country(3254701568, 3254702079, "ISRAEL"));
            _dict.Add(new Ip2Country(3254882560, 3254882815, "ISRAEL"));
            _dict.Add(new Ip2Country(3255323648, 3255324159, "ISRAEL"));
            _dict.Add(new Ip2Country(3255326720, 3255327231, "ISRAEL"));
            _dict.Add(new Ip2Country(3256688640, 3256692735, "ISRAEL"));
            _dict.Add(new Ip2Country(3258074880, 3258075135, "ISRAEL"));
            _dict.Add(new Ip2Country(3258101504, 3258101759, "ISRAEL"));
            _dict.Add(new Ip2Country(3258361856, 3258362879, "ISRAEL"));
            _dict.Add(new Ip2Country(3258504960, 3258505215, "ISRAEL"));
            _dict.Add(new Ip2Country(3260581888, 3260582399, "ISRAEL"));
            _dict.Add(new Ip2Country(3260678144, 3260743679, "ISRAEL"));
            _dict.Add(new Ip2Country(3262052608, 3262052863, "ISRAEL"));
            _dict.Add(new Ip2Country(3262439936, 3262440447, "ISRAEL"));
            _dict.Add(new Ip2Country(3262440960, 3262441471, "ISRAEL"));
            _dict.Add(new Ip2Country(3262476048, 3262476055, "ISRAEL"));
            _dict.Add(new Ip2Country(3262476532, 3262476535, "ISRAEL"));
            _dict.Add(new Ip2Country(3262478159, 3262478159, "ISRAEL"));
            _dict.Add(new Ip2Country(3262478452, 3262478452, "ISRAEL"));
            _dict.Add(new Ip2Country(3262479125, 3262479125, "ISRAEL"));
            _dict.Add(new Ip2Country(3262479553, 3262479553, "ISRAEL"));
            _dict.Add(new Ip2Country(3263543296, 3263545343, "ISRAEL"));
            _dict.Add(new Ip2Country(3264666112, 3264666623, "ISRAEL"));
            _dict.Add(new Ip2Country(3264832768, 3264833023, "ISRAEL"));
            _dict.Add(new Ip2Country(3265369344, 3265369599, "ISRAEL"));
            _dict.Add(new Ip2Country(3267508736, 3267508991, "ISRAEL"));
            _dict.Add(new Ip2Country(3267556864, 3267557375, "ISRAEL"));
            _dict.Add(new Ip2Country(3267557888, 3267558399, "ISRAEL"));
            _dict.Add(new Ip2Country(3268740096, 3268740351, "ISRAEL"));
            _dict.Add(new Ip2Country(3271382528, 3271383039, "ISRAEL"));
            _dict.Add(new Ip2Country(3271915520, 3271916031, "ISRAEL"));
            _dict.Add(new Ip2Country(3272262144, 3272262399, "ISRAEL"));
            _dict.Add(new Ip2Country(3272268800, 3272269055, "ISRAEL"));
            _dict.Add(new Ip2Country(3272269312, 3272269567, "ISRAEL"));
            _dict.Add(new Ip2Country(3272426656, 3272426671, "ISRAEL"));
            _dict.Add(new Ip2Country(3273036800, 3273037311, "ISRAEL"));
            _dict.Add(new Ip2Country(3273434624, 3273435135, "ISRAEL"));
            _dict.Add(new Ip2Country(3273438208, 3273438719, "ISRAEL"));
            _dict.Add(new Ip2Country(3274347520, 3274348031, "ISRAEL"));
            _dict.Add(new Ip2Country(3275534336, 3275534847, "ISRAEL"));
            _dict.Add(new Ip2Country(3275548672, 3275549695, "ISRAEL"));
            _dict.Add(new Ip2Country(3275604736, 3275604767, "ISRAEL"));
            _dict.Add(new Ip2Country(3275624960, 3275625471, "ISRAEL"));
            _dict.Add(new Ip2Country(3275912704, 3275912959, "ISRAEL"));
            _dict.Add(new Ip2Country(3276139520, 3276140543, "ISRAEL"));
            _dict.Add(new Ip2Country(3276305408, 3276308479, "ISRAEL"));
            _dict.Add(new Ip2Country(3276751232, 3276751295, "ISRAEL"));
            _dict.Add(new Ip2Country(3276785664, 3276785791, "ISRAEL"));
            _dict.Add(new Ip2Country(3276963840, 3276964351, "ISRAEL"));
            _dict.Add(new Ip2Country(3277464832, 3277465087, "ISRAEL"));
            _dict.Add(new Ip2Country(3277472608, 3277472623, "ISRAEL"));
            _dict.Add(new Ip2Country(3277711872, 3277712383, "ISRAEL"));
            _dict.Add(new Ip2Country(3277829888, 3277830143, "ISRAEL"));
            _dict.Add(new Ip2Country(3277841920, 3277842431, "ISRAEL"));
            _dict.Add(new Ip2Country(3278943549, 3278943549, "ISRAEL"));
            _dict.Add(new Ip2Country(3278943742, 3278943742, "ISRAEL"));
            _dict.Add(new Ip2Country(3278943875, 3278943875, "ISRAEL"));
            _dict.Add(new Ip2Country(3279295744, 3279295999, "ISRAEL"));
            _dict.Add(new Ip2Country(3279990784, 3279991295, "ISRAEL"));
            _dict.Add(new Ip2Country(3280577536, 3280577791, "ISRAEL"));
            _dict.Add(new Ip2Country(3282104320, 3282105343, "ISRAEL"));
            _dict.Add(new Ip2Country(3283184640, 3283185663, "ISRAEL"));
            _dict.Add(new Ip2Country(3283493120, 3283493375, "ISRAEL"));
            _dict.Add(new Ip2Country(3283651520, 3283651583, "ISRAEL"));
            _dict.Add(new Ip2Country(3283979264, 3283979775, "ISRAEL"));
            _dict.Add(new Ip2Country(3284013824, 3284014079, "ISRAEL"));
            _dict.Add(new Ip2Country(3284014848, 3284015103, "ISRAEL"));
            _dict.Add(new Ip2Country(3284687872, 3284688383, "ISRAEL"));
            _dict.Add(new Ip2Country(3284716800, 3284717055, "ISRAEL"));
            _dict.Add(new Ip2Country(3285502672, 3285502703, "ISRAEL"));
            _dict.Add(new Ip2Country(3285516720, 3285516735, "ISRAEL"));
            _dict.Add(new Ip2Country(3285526016, 3285526023, "ISRAEL"));
            _dict.Add(new Ip2Country(3285527456, 3285527487, "ISRAEL"));
            _dict.Add(new Ip2Country(3285527552, 3285527615, "ISRAEL"));
            _dict.Add(new Ip2Country(3285529200, 3285529215, "ISRAEL"));
            _dict.Add(new Ip2Country(3285531552, 3285531567, "ISRAEL"));
            _dict.Add(new Ip2Country(3285533952, 3285534079, "ISRAEL"));
            _dict.Add(new Ip2Country(3285536576, 3285536607, "ISRAEL"));
            _dict.Add(new Ip2Country(3285536756, 3285536759, "ISRAEL"));
            _dict.Add(new Ip2Country(3285537024, 3285537087, "ISRAEL"));
            _dict.Add(new Ip2Country(3285540112, 3285540127, "ISRAEL"));
            _dict.Add(new Ip2Country(3285544192, 3285544255, "ISRAEL"));
            _dict.Add(new Ip2Country(3285548840, 3285548847, "ISRAEL"));
            _dict.Add(new Ip2Country(3285555328, 3285555455, "ISRAEL"));
            _dict.Add(new Ip2Country(3285573888, 3285573951, "ISRAEL"));
            _dict.Add(new Ip2Country(3285574400, 3285574655, "ISRAEL"));
            _dict.Add(new Ip2Country(3285574912, 3285575167, "ISRAEL"));
            _dict.Add(new Ip2Country(3285575424, 3285575679, "ISRAEL"));
            _dict.Add(new Ip2Country(3285777408, 3285777663, "ISRAEL"));
            _dict.Add(new Ip2Country(3286314496, 3286315007, "ISRAEL"));
            _dict.Add(new Ip2Country(3286318592, 3286319103, "ISRAEL"));
            _dict.Add(new Ip2Country(3286932992, 3286933503, "ISRAEL"));
            _dict.Add(new Ip2Country(3287447040, 3287447551, "ISRAEL"));
            _dict.Add(new Ip2Country(3287461376, 3287461631, "ISRAEL"));
            _dict.Add(new Ip2Country(3287553536, 3287554047, "ISRAEL"));
            _dict.Add(new Ip2Country(3287641088, 3287641599, "ISRAEL"));
            _dict.Add(new Ip2Country(3287949568, 3287949823, "ISRAEL"));
            _dict.Add(new Ip2Country(3287953152, 3287953407, "ISRAEL"));
            _dict.Add(new Ip2Country(3351971840, 3351972095, "ISRAEL"));
            _dict.Add(new Ip2Country(3351972864, 3351973119, "ISRAEL"));
            _dict.Add(new Ip2Country(3351975936, 3351976447, "ISRAEL"));
            _dict.Add(new Ip2Country(3351976704, 3351976959, "ISRAEL"));
            _dict.Add(new Ip2Country(3351979520, 3351979583, "ISRAEL"));
            _dict.Add(new Ip2Country(3351986944, 3351987199, "ISRAEL"));
            _dict.Add(new Ip2Country(3351995648, 3351995903, "ISRAEL"));
            _dict.Add(new Ip2Country(3351996928, 3351997439, "ISRAEL"));
            _dict.Add(new Ip2Country(3352004352, 3352004607, "ISRAEL"));
            _dict.Add(new Ip2Country(3352005888, 3352006143, "ISRAEL"));
            _dict.Add(new Ip2Country(3352006400, 3352006527, "ISRAEL"));
            _dict.Add(new Ip2Country(3352006592, 3352006655, "ISRAEL"));
            _dict.Add(new Ip2Country(3352007360, 3352007367, "ISRAEL"));
            _dict.Add(new Ip2Country(3352007392, 3352007423, "ISRAEL"));
            _dict.Add(new Ip2Country(3352007584, 3352007679, "ISRAEL"));
            _dict.Add(new Ip2Country(3352007968, 3352008063, "ISRAEL"));
            _dict.Add(new Ip2Country(3352009312, 3352009343, "ISRAEL"));
            _dict.Add(new Ip2Country(3352010816, 3352010879, "ISRAEL"));
            _dict.Add(new Ip2Country(3352011200, 3352011263, "ISRAEL"));
            _dict.Add(new Ip2Country(3352014720, 3352014847, "ISRAEL"));
            _dict.Add(new Ip2Country(3352014928, 3352014943, "ISRAEL"));
            _dict.Add(new Ip2Country(3352014976, 3352015103, "ISRAEL"));
            _dict.Add(new Ip2Country(3352015840, 3352015871, "ISRAEL"));
            _dict.Add(new Ip2Country(3352016000, 3352016127, "ISRAEL"));
            _dict.Add(new Ip2Country(3352020672, 3352020703, "ISRAEL"));
            _dict.Add(new Ip2Country(3352020736, 3352020863, "ISRAEL"));
            _dict.Add(new Ip2Country(3352023040, 3352023167, "ISRAEL"));
            _dict.Add(new Ip2Country(3352023344, 3352023359, "ISRAEL"));
            _dict.Add(new Ip2Country(3352023680, 3352023807, "ISRAEL"));
            _dict.Add(new Ip2Country(3352024320, 3352024447, "ISRAEL"));
            _dict.Add(new Ip2Country(3352025088, 3352025215, "ISRAEL"));
            _dict.Add(new Ip2Country(3352026624, 3352026751, "ISRAEL"));
            _dict.Add(new Ip2Country(3352027296, 3352027327, "ISRAEL"));
            _dict.Add(new Ip2Country(3352028800, 3352028927, "ISRAEL"));
            _dict.Add(new Ip2Country(3352030048, 3352030079, "ISRAEL"));
            _dict.Add(new Ip2Country(3352030144, 3352030175, "ISRAEL"));
            _dict.Add(new Ip2Country(3352030384, 3352030399, "ISRAEL"));
            _dict.Add(new Ip2Country(3352032720, 3352032735, "ISRAEL"));
            _dict.Add(new Ip2Country(3352034304, 3352034815, "ISRAEL"));
            _dict.Add(new Ip2Country(3426013184, 3426013439, "ISRAEL"));
        }
    }
}