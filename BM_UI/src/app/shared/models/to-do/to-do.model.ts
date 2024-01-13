export interface ToDo{
  id:string;
  text:string;
  startDate:Date;
  endDate:Date;
  description:string;
  companyId:string;
  recurrenceRule:string;
  allDay:boolean;
}
