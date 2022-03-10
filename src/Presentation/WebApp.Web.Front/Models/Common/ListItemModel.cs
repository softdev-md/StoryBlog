namespace WebApp.Web.Front.Models.Common
{
    public class ListItemModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ListItemModel"/>.
        /// </summary>
        public ListItemModel() { }

        /// <summary>
        /// Initializes a new instance of <see cref="ListItemModel"/>.
        /// </summary>
        /// <param name="text">The display text of this <see cref="ListItemModel"/>.</param>
        /// <param name="value">The value of this <see cref="ListItemModel"/>.</param>
        public ListItemModel(string text, object value)
            : this()
        {
            Text = text;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ListItemModel"/>.
        /// </summary>
        /// <param name="text">The display text of this <see cref="ListItemModel"/>.</param>
        /// <param name="value">The value of this <see cref="ListItemModel"/>.</param>
        /// <param name="selected">Value that indicates whether this <see cref="ListItemModel"/> is selected.</param>
        public ListItemModel(string text, object value, bool selected)
            : this(text, value)
        {
            Selected = selected;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ListItemModel"/>.
        /// </summary>
        /// <param name="text">The display text of this <see cref="ListItemModel"/>.</param>
        /// <param name="value">The value of this <see cref="ListItemModel"/>.</param>
        /// <param name="selected">Value that indicates whether this <see cref="ListItemModel"/> is selected.</param>
        /// <param name="disabled">Value that indicates whether this <see cref="ListItemModel"/> is disabled.</param>
        public ListItemModel(string text, object value, bool selected, bool disabled)
            : this(text, value, selected)
        {
            Disabled = disabled;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="ListItemModel"/> is disabled.
        /// This property is typically rendered as a <c>disabled="disabled"</c> attribute in the HTML
        /// <c>&lt;option&gt;</c> element.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="ListItemModel"/> is selected.
        /// This property is typically rendered as a <c>selected="selected"</c> attribute in the HTML
        /// <c>&lt;option&gt;</c> element.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the display text of this <see cref="ListItemModel"/>.
        /// This property is typically rendered as the inner HTML in the HTML <c>&lt;option&gt;</c> element.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the value of this <see cref="ListItemModel"/>.
        /// This property is typically rendered as a <c>value="..."</c> attribute in the HTML
        /// <c>&lt;option&gt;</c> element.
        /// </summary>
        public object Value { get; set; }
    }
}
