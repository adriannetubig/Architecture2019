export interface PagedList<T> {
  numberOfPages: number;
  itemsPerPage: number;
  numberOfItems: number;
  pageNo: number;
  items: T[];
}
