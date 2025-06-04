export interface CoreStudent {
    id: string;
    name: string;
    email: string;
    password: string | null;
    phone: string;
  }
  
  export interface AcademicalStudent {
    stupin: string;
    group: string;
    formOfStudy: string;
    formOfPay: string;
    terminOfStudy: string;
    endOfStudy: string;
  }
  
  export interface Student {
    coreStudent: CoreStudent;
    academicalStudent: AcademicalStudent;
  }
  