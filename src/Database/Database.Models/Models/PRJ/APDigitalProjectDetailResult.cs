using System;
using System.Collections.Generic;

namespace Database.Models.PRJ
{
    public class APDigitalProjectDetailResult
    {
        public class Root
        {
            public int statusCode { get; set; }
            public string message { get; set; }
            public string code { get; set; }
            public Data data { get; set; }
        }
        public class AddressComponents
        {
            public string house_number { get; set; }
            public string street { get; set; }
            public string sub_district { get; set; }
            public string district { get; set; }
            public string province { get; set; }
            public int postal_code { get; set; }
        }

        public class Area
        {
            public int? id { get; set; }
            public int? location_id { get; set; }
            public string locale { get; set; }
            public string name { get; set; }
            public int is_published { get; set; }
        }

        public class Area2
        {
            public string id { get; set; }
            public string location_id { get; set; }
            public string locale { get; set; }
            public string name { get; set; }
            public int is_published { get; set; }
        }

        public class BannerDesktop
        {
            public string url { get; set; }
            public string alt { get; set; }
            public string link { get; set; }
        }

        public class BannerMobile
        {
            public string url { get; set; }
            public string alt { get; set; }
            public string link { get; set; }
        }

        public class Brand
        {
            public string mode { get; set; }
            public BannerDesktop banner_desktop { get; set; }
            public BannerMobile banner_mobile { get; set; }
            public string logo { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string title { get; set; }
            public string vdo_youtube_url { get; set; }
            public string starting_price { get; set; }
            public string detail { get; set; }
            public string color { get; set; }
        }

        public class Building
        {
            public int id { get; set; }
            public string title { get; set; }
            public List<FloorPlan> floor_plans { get; set; }
        }

        public class ColorPalette
        {
            public string background_color { get; set; }
            public string title_color { get; set; }
            public string description_color { get; set; }
        }

        public class Concept
        {
            public int id { get; set; }
            public string locale { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public int showTime { get; set; }
            public int show_time { get; set; }
            public Image image { get; set; }
        }

        public class Data
        {
            public string id { get; set; }
            public string project_id { get; set; }
            public string locale { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string status { get; set; }
            public string short_description { get; set; }
            public string description { get; set; }
            public string selling_point { get; set; }
            public bool line_status { get; set; }
            public string line_text { get; set; }
            public string line_url { get; set; }
            public string location_lat { get; set; }
            public string location_lng { get; set; }
            public ProjectType project_type { get; set; }
            public Brand brand { get; set; }
            public List<PromotionDetail> promotion_details { get; set; }
            public LineQrCodeImage line_qr_code_image { get; set; }
            public List<HighlightBanner> highlight_banners { get; set; }
            public Infos infos { get; set; }
            public Location location { get; set; }
            public Plan plan { get; set; }
            public Virtual @virtual { get; set; }
            public ProjectThumbnail project_thumbnail { get; set; }
        }

        public class Desktop
        {
            public string url { get; set; }
            public string alt { get; set; }
        }

        public class Facility
        {
            public int id { get; set; }
            public string locale { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string metadata { get; set; }
            public Image image { get; set; }
        }

        public class Factsheet
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
        }

        public class FloorPlan
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public Metadata metadata { get; set; }
            public Image image { get; set; }
        }

        public class Gallery
        {
            public string id { get; set; }
            public string locale { get; set; }
            public string project_translation_id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string metadata { get; set; }
            public Image image { get; set; }
        }

        public class HighlightBanner
        {
            public int id { get; set; }
            public string name { get; set; }
            public string banner_mode { get; set; }
            public BannerDesktop banner_desktop { get; set; }
            public BannerMobile banner_mobile { get; set; }
            public string video_url { get; set; }
            public int show_time { get; set; }
            public TextBox text_box { get; set; }
            public LineInfos line_infos { get; set; }
        }

        public class HighlightLocation
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
        }

        public class Image
        {
            public Desktop desktop { get; set; }
            public Mobile mobile { get; set; }
            public string url { get; set; }
            public string alt { get; set; }
            public string link { get; set; }
        }

        public class Infos
        {
            public string factsheet_brochure { get; set; }
            public List<Factsheet> factsheets { get; set; }
            public List<Concept> concepts { get; set; }
            public List<Facility> facilities { get; set; }
            public List<HighlightLocation> highlight_locations { get; set; }
            public List<Progress> progress { get; set; }
            public List<Gallery> galleries { get; set; }
            public List<ProgressGallery> progress_galleries { get; set; }
        }

        public class LineInfos
        {
            public string text { get; set; }
            public string url { get; set; }
            public QrCode qr_code { get; set; }
        }

        public class LineQrCodeImage
        {
            public string url { get; set; }
            public string alt { get; set; }
        }

        public class Location
        {
            public Area area { get; set; }
            public List<Area> areas { get; set; }
            public string address { get; set; }
            public AddressComponents address_components { get; set; }
            public Map map { get; set; }
        }

        public class Map
        {
            public Image image { get; set; }
            public string lat { get; set; }
            public string lng { get; set; }
        }

        public class Metadata
        {
            public string floor_level_start { get; set; }
            public string floor_level_end { get; set; }
        }

        public class Mobile
        {
            public string url { get; set; }
            public string alt { get; set; }
        }

        public class Plan
        {
            public List<Building> buildings { get; set; }
            public List<UnitType> unit_types { get; set; }
        }

        public class Progress
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string image { get; set; }
        }

        public class ProgressGallery
        {
            public string id { get; set; }
            public string locale { get; set; }
            public string project_translation_id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string metadata { get; set; }
            public Image image { get; set; }
        }

        public class ProjectThumbnail
        {
            public string url { get; set; }
            public string alt { get; set; }
        }

        public class ProjectType
        {
            public string id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
        }

        public class PromotionDetail
        {
            public int id { get; set; }
            public string locale { get; set; }
            public string condition { get; set; }
            public string slug { get; set; }
            public string detail { get; set; }
            public DateTime? promotion_started_at { get; set; }
            public DateTime? promotion_ended_at { get; set; }
        }

        public class QrCode
        {
            public string url { get; set; }
            public string alt { get; set; }
            public string link { get; set; }
        }

        public class TextBox
        {
            public string logo { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public ColorPalette color_palette { get; set; }
            public string position { get; set; }
        }

        public class UnitPlan
        {
            public int id { get; set; }
            public string floor { get; set; }
            public Image image { get; set; }
        }

        public class UnitType
        {
            public int id { get; set; }
            public int parent { get; set; }
            public string locale { get; set; }
            public string slug { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string short_description { get; set; }
            public string description { get; set; }
            public int priority { get; set; }
            public List<UnitPlan> unit_plans { get; set; }
        }

        public class Virtual
        {
            public string tour_url { get; set; }
            public string unit_url { get; set; }
            public string facility_url { get; set; }
        }
    }
}
