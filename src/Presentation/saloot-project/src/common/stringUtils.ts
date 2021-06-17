export const capitalizeFirstLetter = (str: string): string => {
  return str.charAt(0).toUpperCase() + str.slice(1);
};

export const isNullOrEmpty = (str: string): boolean => {
  if (str == undefined || str == "" || str.length == 0 || !str) {
    return true;
  }

  return false;
};
