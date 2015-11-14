namespace CFH.Models
{
    using System.Collections.Generic;

    public class Directory
    {
        private ICollection<File> files;

        public Directory()
        {
            this.files = new HashSet<File>();

        }

        public int DirId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<File> Files
        {
            get
            {
                return this.files;
            }

            set
            {
                this.files = value;
            }
        }
    }
}
