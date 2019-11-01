export interface PageFilter<T> {
  pageNo: number;
  itemsPerPage: number;
  filter: T;
}
