export const  isEmpty = (value) => {
    return (value == null || value === '' || !value.trim());
  }

  export const  isNullOrEmpty = (value) => {
    return (value == null || value === '' );
  }