export interface CoreInfo {
    id: string;
    name: string;
    email: string;
    phone: string;
    departament: string | null;
    title: string | null;
    photoPath: string;
  }
  
  export interface AcademicDetails {
    stupin: string;
    rank: string;
    departament: string;
    title: string;
  }
  
  export interface AdditionalInfo {
    research: string;
    subjects: string;
    biography: string;
    projects: string;
  }
  
  export interface Teacher {
    coreInfo: CoreInfo;
    academicDetails: AcademicDetails;
    additionalInfo: AdditionalInfo;
  }
  