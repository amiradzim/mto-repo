namespace API.Entities
{
    public class CsvLine
    {
        public int Id { get; set; }
        public string ProjName { get; set; }
        public string PlatNo { get; set; }
        public string PlatType { get; set; }
        public string PlatArea { get; set; }
        public string SubArea { get; set; }
        public string MatType { get; set; }
        public string MatVariant { get; set; }
        public string ProcMethod { get; set; }
        public string DwgNo { get; set; }
        public string DwgCode { get; set; }
        public string MatGroup { get; set; }
        public string Description { get; set; }
        public long Diameter { get; set; }
        public long Thickness { get; set; }
        public long Nal { get; set; }
        public long UnitWeight { get; set; }
        public long BaseWeight { get; set; }
        public long SurfaceArea { get; set; }

        public override string ToString()
        {
            return  $"Id: {Id}, " +
                    $"ProjName: {ProjName}, " +
                    $"PlatNo: {PlatNo}, " +
                    $"PlatType: {PlatType}, " +
                    $"PlatArea: {PlatArea}, " +
                    $"SubArea: {SubArea}, " +
                    $"MatType: {MatType}, " +
                    $"MatVariant: {MatVariant}, " +
                    $"ProcMethod: {ProcMethod}, " +
                    $"DwgNo: {DwgNo}, " +
                    $"DwgCode: {DwgCode}, " +
                    $"MatGroup: {MatGroup}, " +
                    $"Description: {Description}, " +
                    $"Diameter: {Diameter}, " +
                    $"Thickness: {Thickness}, " +
                    $"Nal: {Nal}, " +
                    $"UnitWeight: {UnitWeight}, " +
                    $"BaseWeight: {BaseWeight}, " +
                    $"SurfaceArea: {SurfaceArea}";
        }
    }
}