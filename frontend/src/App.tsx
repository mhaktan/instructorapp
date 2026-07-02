import React, { useState } from 'react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import { isAuthenticated, clearAuth } from './dataProvider';
import { LoginPage } from './screens/LoginPage';
import { GlobalMenu } from './components/GlobalMenu';
import { InstructorList } from './screens/Instructor/InstructorList';
import { MemberList } from './screens/Member/MemberList';
import { MemberInstructorList } from './screens/MemberInstructor/MemberInstructorList';
import { LessonList } from './screens/Lesson/LessonList';
import { LessonAttendanceList } from './screens/LessonAttendance/LessonAttendanceList';
import { PaymentList } from './screens/Payment/PaymentList';
import { DashboardScreen } from './screens/dashboard/DashboardScreen';
import UserListScreen from './admin/UserListScreen';
import RoleListScreen from './admin/RoleListScreen';

const queryClient = new QueryClient({
  defaultOptions: { queries: { retry: 1, staleTime: 0 } },
});

export const App: React.FC = () => {
  const [authenticated, setAuthenticated] = useState(isAuthenticated());


  const handleLogout = () => {
    clearAuth();
    setAuthenticated(false);
    queryClient.clear();
  };

  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path="*" element={
            authenticated
              ? (
                <GlobalMenu>
                  <Routes>
                    <Route path="/" element={<Navigate to="/dashboard" replace />} />
          <Route path="/dashboard" element={<DashboardScreen />} />
          <Route path="/Instructor" element={<InstructorList />} />
          <Route path="/Member" element={<MemberList />} />
          <Route path="/MemberInstructor" element={<MemberInstructorList />} />
          <Route path="/Lesson" element={<LessonList />} />
          <Route path="/LessonAttendance" element={<LessonAttendanceList />} />
          <Route path="/Payment" element={<PaymentList />} />
          <Route path="/users" element={<UserListScreen />} />
          <Route path="/roles" element={<RoleListScreen />} />
                  </Routes>
                </GlobalMenu>
              )
              : <LoginPage onLogin={() => setAuthenticated(true)} />
          } />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
};
